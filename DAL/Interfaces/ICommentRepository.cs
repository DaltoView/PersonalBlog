using DAL.Entities;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetCommentsByPostId(Guid id);
        void EditComment(Comment comment);
    }
}