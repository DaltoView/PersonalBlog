using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context)
            : base(context)
        {
        }

        public IEnumerable<Comment> GetCommentsByPostId(Guid id)
        {
            var post = context.Posts.Find(id);
            return post.Comments.OrderByDescending(p => p.PostDate).ToList();
        }

        public void EditComment(Comment comment)
        {
            var existingComment = context.Comments.Find(comment.Id);

            existingComment.Text = comment.Text;
            existingComment.EditDate = comment.EditDate;
        }
    }
}