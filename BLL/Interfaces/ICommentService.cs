using BLL.DTO;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICommentService : IDisposable
    {
        IEnumerable<CommentDTO> GetCommentsByPostId(Guid id);
        CommentDTO GetCommentById(Guid id);
        void CreateComment(CommentDTO comment);
        void EditComment(CommentDTO comment);
        void DeleteComment(Guid id);
    }
}