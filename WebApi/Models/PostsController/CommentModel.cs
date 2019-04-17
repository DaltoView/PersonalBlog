using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.PostsController
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "The comment lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
    }
}