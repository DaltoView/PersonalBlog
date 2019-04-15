using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }

        public IEnumerable<TagDTO> Tags { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }

        public Guid AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
