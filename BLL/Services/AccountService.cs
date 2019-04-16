using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Security.Claims;

namespace BLL.Services
{
    public class AccountService : BaseService<UserDTO, User>, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            mapper = AutomapperConfigs.AutomapperConfigs.GetAccountServiceMapper();
        }

        public OperationDetails RegisterUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            User appUser = mapper.Map<UserDTO, User>(user);

            IdentityResult result = UnitOfWork.UserManager.Create(appUser, user.Password);
            if (result.Succeeded)
            {
                var userId = UnitOfWork.UserManager.Find(user.UserName, user.Password).Id;

                if (userId != null)
                {
                    result = UnitOfWork.UserManager.AddToRole(userId, "User");
                }
            }

            if (!result.Succeeded)
            {
                return new OperationDetails(false, result.Errors.First());
            }

            UnitOfWork.Save();
            return new OperationDetails(true);
        }

        public UserDTO FindUser(string userName, string password)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));

            if (password == null)
                throw new ArgumentNullException(nameof(password));


            User user = UnitOfWork.UserManager.Find(userName, password);
            UserDTO userDto = mapper.Map<User, UserDTO>(user);

            if(userDto != null)
                userDto.Role = GetRoleByUserId(user.Id);

            return userDto;
        }

        public OperationDetails EditUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = UnitOfWork.UserManager.FindById(user.Id);
            var existingAuthor = UnitOfWork.Authors.Get(existingUser.Author.Id);

            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingAuthor.FirstName = user.Author.FirstName;
            existingAuthor.LastName = user.Author.LastName;
            existingAuthor.Birthdate = user.Author.Birthdate;

            UnitOfWork.Authors.Update(existingAuthor);
            var result = UnitOfWork.UserManager.Update(existingUser);

            if (!result.Succeeded)
            {
                return new OperationDetails(false, result.Errors.First());
            }

            UnitOfWork.Save();
            return new OperationDetails(true);
        }

        public string GetRoleByUserId(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            return UnitOfWork.UserManager.GetRoles(userId).FirstOrDefault();

        }

        public ClaimsIdentity CreateIdentity(Guid userId, string authenticationType)
        {
            if (authenticationType == null)
                throw new ArgumentNullException(nameof(authenticationType));

            User user = UnitOfWork.UserManager.FindById(userId);

            return UnitOfWork.UserManager.CreateIdentity(user, authenticationType);
        }

        public UserDTO GetUserById(Guid userId)
        {
            User user = UnitOfWork.UserManager.FindById(userId);
            UserDTO userDto = mapper.Map<User, UserDTO>(user);
            userDto.Role = GetRoleByUserId(user.Id);
            return userDto;
        }

        public UserDTO GetUserByName(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));

            User user = UnitOfWork.UserManager.FindByName(userName);
            UserDTO userDto = mapper.Map<User, UserDTO>(user);
            userDto.Role = GetRoleByUserId(user.Id);
            return userDto;
        }

        public bool IsInRole(Guid userId, string role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return UnitOfWork.UserManager.IsInRole(userId, role);
        }

        public OperationDetails DeleteUser(Guid id)
        {
            var user = UnitOfWork.UserManager.FindById(id);

            UnitOfWork.Comments.RemoveRange(user.Author.Comments);
            UnitOfWork.Posts.RemoveRange(user.Author.Posts);
            UnitOfWork.Authors.Remove(user.Author.Id);

            IdentityResult result = UnitOfWork.UserManager.Delete(user);
            if (result.Succeeded)
            {
                UnitOfWork.Save();
                return new OperationDetails(true);
            }
            else
            {
                return new OperationDetails(false, result.Errors.First());
            }
        }

        public OperationDetails DeleteUser(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));

            var user = UnitOfWork.UserManager.FindByName(userName);
            var logins = user.Logins;

            foreach (var login in logins)
            {
                UnitOfWork.UserManager.RemoveLogin(user.Id, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            var rolesForUser = UnitOfWork.UserManager.GetRoles(user.Id);
            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser)
                {
                    UnitOfWork.UserManager.RemoveFromRole(user.Id, item);
                }
            }

            IdentityResult result = UnitOfWork.UserManager.Delete(user);
            if (result.Succeeded)
            {
                UnitOfWork.Save();
                return new OperationDetails(true);
            }
            else
            {
                return new OperationDetails(false, result.Errors.First());
            }
        }
    }
}