using BLL.DTO;
using BLL.DTO.Filters;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for performing actions on users.
    /// </summary>
    public class UserService : BaseService<UserDTO, User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            mapper = AutomapperConfigs.AutomapperConfigs.GetUserServiceMapper();
        }

        /// <summary>
        /// Gets all users by filter parameters.
        /// </summary>
        /// <param name="userSearchDTO"></param>
        /// <returns></returns>
        public IEnumerable<UserDTO> GetAllUsers(UserFilterDTO userSearchDTO)
        {
            if (string.IsNullOrEmpty(userSearchDTO.SortOrder))
                userSearchDTO.SortOrder = "username_asc";

            var users = UnitOfWork.UserManager.Users.AsQueryable();

            if(userSearchDTO != null)
            {
                if (!string.IsNullOrEmpty(userSearchDTO.Search))
                {
                    userSearchDTO.Search = userSearchDTO.Search.ToLower();
                    var usersAll = users;
                    users = users.Where(p => p.Email.ToLower().Contains(userSearchDTO.Search));
                    users = users.Union(usersAll.Where(p => p.UserName.ToLower().Contains(userSearchDTO.Search)));
                    users = users.Union(usersAll.Where(p => p.Author.FirstName.ToLower().Contains(userSearchDTO.Search)));
                    users = users.Union(usersAll.Where(p => p.Author.LastName.ToLower().Contains(userSearchDTO.Search)));
                }
                if (userSearchDTO.AgeFrom.HasValue)
                {
                    var dateFrom = ConvertAgeToDate(userSearchDTO.AgeFrom.Value);
                    users = users.Where(p => p.Author.Birthdate <= dateFrom);
                }
                if (userSearchDTO.AgeTo.HasValue)
                {
                    var dateTo = ConvertAgeToDate(userSearchDTO.AgeTo.Value);
                    users = users.Where(p => p.Author.Birthdate >= dateTo);
                }
                if (!string.IsNullOrEmpty(userSearchDTO.Role))
                {
                    var role = UnitOfWork.RoleManager.FindByName(userSearchDTO.Role);
                    users = users.Where(p => p.Roles.FirstOrDefault().RoleId == role.Id);
                }
            }

            if (!string.IsNullOrEmpty(userSearchDTO.SortOrder))
            {
                switch (userSearchDTO.SortOrder.ToLower())
                {
                    case "firstname_asc":
                        users = users.OrderBy(p => p.Author.FirstName);
                        break;
                    case "firstname_desc":
                        users = users.OrderByDescending(p => p.Author.FirstName);
                        break;
                    case "lastname_asc":
                        users = users.OrderBy(p => p.Author.LastName);
                        break;
                    case "lastname_desc":
                        users = users.OrderByDescending(p => p.Author.LastName);
                        break;
                    case "age_asc":
                        users = users.OrderBy(p => p.Author.Birthdate);
                        break;
                    case "age_desc":
                        users = users.OrderByDescending(p => p.Author.Birthdate);
                        break;
                    case "username_asc":
                        users = users.OrderBy(p => p.UserName);
                        break;
                    case "username_desc":
                        users = users.OrderByDescending(p => p.UserName);
                        break;
                    default:
                        users = users.OrderBy(p => p.UserName);
                        break;
                }
            }

            if (userSearchDTO.Skip.HasValue)
                users = users.Skip(userSearchDTO.Skip.Value);

            if (userSearchDTO.Take.HasValue)
                users = users.Take(userSearchDTO.Take.Value);

            var roles = UnitOfWork.RoleManager.Roles.ToList();
            var result = users.ToList()
                .Join(roles,
                user => user.Roles.FirstOrDefault()?.RoleId,
                role => role.Id,
                (user, role) => new UserDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Role = role.Name,
                    Author = mapper.Map<Author, AuthorDTO>(user.Author)
                }
                );

            return result;
        }

        public UserDTO GetUserById(Guid id)
        {
            var user = mapper.Map<User, UserDTO>(UnitOfWork.UserManager.FindById(id));
            user.Role = UnitOfWork.UserManager.GetRoles(id)?.FirstOrDefault();
            return user;
        }

        public OperationDetails CreateUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            User entityUser = mapper.Map<UserDTO, User>(user);

            IdentityResult result = UnitOfWork.UserManager.Create(entityUser, user.Password);
            if (result.Succeeded)
            {
                var userId = UnitOfWork.UserManager.Find(user.UserName, user.Password).Id;

                if (userId != null)
                {
                    result = UnitOfWork.UserManager.AddToRole(userId, user.Role);
                }
            }

            if (!result.Succeeded)
            {
                return new OperationDetails(false, result.Errors.First());
            }

            UnitOfWork.Save();
            return new OperationDetails(true);
        }

        /// <summary>
        /// Edits user (email, username, firstname, lastname, birthdate).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public OperationDetails EditUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = UnitOfWork.UserManager.FindById(user.Id);

            if(existingUser == null)
                return new OperationDetails(false, "User not found");

            var existingAuthor = UnitOfWork.Authors.Get(existingUser.Author.Id);

            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingAuthor.FirstName = user.Author.FirstName;
            existingAuthor.LastName = user.Author.LastName;
            existingAuthor.Birthdate = user.Author.Birthdate;

            UnitOfWork.Authors.Update(existingAuthor);
            var result = UnitOfWork.UserManager.Update(existingUser);

            if (result.Succeeded)
            {
                var userRoles = UnitOfWork.UserManager.GetRoles(user.Id);

                foreach(var role in userRoles)
                {
                    UnitOfWork.UserManager.RemoveFromRole(user.Id, role);
                }

                result = UnitOfWork.UserManager.AddToRole(user.Id, user.Role);
            }

            if (!result.Succeeded)
            {
                return new OperationDetails(false, result.Errors.First());
            }

            UnitOfWork.Save();
            return new OperationDetails(true);
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

        private DateTime ConvertAgeToDate(int age)
        {
            return DateTime.UtcNow.AddYears(-age);
        }
    }
}