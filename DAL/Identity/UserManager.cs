using DAL.EFContext;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DAL.Identity
{
    public class UserManager : UserManager<User, Guid>
    {
        public UserManager(IQueryableUserStore<User, Guid> store)
           : base(store)
        {
        }

        public static UserManager Create(BlogContext context)
        {
            var manager = new UserManager(new UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>(context));

            //Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            return manager;
        }
    }
}
