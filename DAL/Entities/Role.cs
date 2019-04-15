using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DAL.Entities
{
    public class Role : IdentityRole<Guid, UserRole>
    {
    }
}
