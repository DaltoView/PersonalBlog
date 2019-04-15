using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.PostsController
{
    public class PostCreateModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The title lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        public string Article { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }

        public Guid AuthorId { get; set; }
    }
}