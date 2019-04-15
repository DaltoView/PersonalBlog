using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Article { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
