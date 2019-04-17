using System;
using System.Collections.Generic;

namespace WebApi.Models.PostsController
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }

        public Guid AuthorId { get; set; }
        public string FullName { get; set; }
    }
}