using DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAL.EntityConfigurations
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            HasKey(p => p.Id);
            HasMany(p => p.Comments).WithRequired(p => p.Author);
            HasMany(p => p.Posts).WithRequired(p => p.Author).WillCascadeOnDelete(false);
        }
    }
}
