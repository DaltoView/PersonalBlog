using System;

namespace BLL.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }

        public Guid PostId { get; set; }
        public PostDTO Post { get; set; }

        public Guid AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
