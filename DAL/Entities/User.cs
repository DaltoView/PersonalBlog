using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DAL.Entities
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        public override Guid Id { get; set; }

        public virtual Author Author { get; set; }
    }
}
