using System.Collections.Generic;

namespace DAL.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
