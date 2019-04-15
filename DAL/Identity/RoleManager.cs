using DAL.EFContext;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DAL.Identity
{
    public class RoleManager : RoleManager<Role, Guid>
    {
        public RoleManager(IQueryableRoleStore<Role, Guid> store)
            : base(store)
        {
        }

        public static RoleManager Create(BlogContext context)
        {
            var manager = new RoleManager(new RoleStore<Role, Guid, UserRole>(context));

            return manager;
        }
    }
}
