using DAL.Entities;
using DAL.EntityConfigurations;
using DAL.Initializers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace DAL.EFContext
{
    public class BlogContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        static BlogContext()
        {
            Database.SetInitializer(new BlogDbInitializer());
        }

        public BlogContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
