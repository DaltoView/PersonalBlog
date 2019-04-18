using DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DAL.EntityConfigurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(p => p.Tags).WithMany(p => p.Posts);
            HasMany(p => p.Comments).WithRequired(p => p.Post).HasForeignKey(p => p.PostId);
            Property(p => p.Title).IsRequired().HasMaxLength(100);
            Property(p => p.Article).IsRequired();
        }
    }
}
