using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService : IDisposable
    {
        IEnumerable<CommentDTO> GetCommentsByPostId(Guid id);
        void CreateComment(CommentDTO comment);
        void EditComment(CommentDTO comment);
        void DeleteComment(Guid id);
    }
}