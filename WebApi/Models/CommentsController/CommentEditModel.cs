using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.CommentsController
{
    public class CommentEditModel
    {
        public Guid Id { get; set; }
        [StringLength(150, ErrorMessage = "The comment lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string Text { get; set; }
    }
}