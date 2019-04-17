using DAL.EFContext;
using DAL.Entities;
using DAL.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;

namespace DAL.Initializers
{
    public class BlogDbInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            UserManager userManager = UserManager.Create(context);
            RoleManager roleManager = RoleManager.Create(context);

            var userRole = new Role { Name = "User" };
            var moderRole = new Role { Name = "Moder" };
            var adminRole = new Role { Name = "Admin" };

            roleManager.Create(userRole);
            roleManager.Create(moderRole);
            roleManager.Create(adminRole);

            var author = new Author { Birthdate = DateTime.Parse("1997-10-29"), FirstName = "Dima", LastName = "Semenko" };
            var admin = new User { Email = "semenko@gmail.com", UserName = "DaltoView", Author = author };
            string password = "Admin1";

            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(userManager.Find(admin.UserName, password).Id, adminRole.Name);
            }

            base.Seed(context);
        }
    }
}
