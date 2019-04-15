﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.CommentsController
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        [StringLength(150, ErrorMessage = "The comment lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
    }
}