using System;

namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
