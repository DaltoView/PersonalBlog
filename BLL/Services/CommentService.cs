using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for performing actions on comments.
    /// </summary>
    public class CommentService : BaseService<CommentDTO, Comment>, ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            mapper = AutomapperConfigs.AutomapperConfigs.GetPostServiceMapper();
        }

        public IEnumerable<CommentDTO> GetCommentsByPostId(Guid id)
        {
            var comments = UnitOfWork.Comments.GetCommentsByPostId(id);
            return mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }

        /// <summary>
        /// Creates comment and set post date and time to UTC now and edit date and time to default ("1980-01-01").
        /// </summary>
        /// <param name="comment"></param>
        public void CreateComment(CommentDTO comment)
        {
            comment.PostDate = DateTime.UtcNow;
            comment.EditDate = DateTime.Parse("1980-01-01");
            UnitOfWork.Comments.Add(mapper.Map<CommentDTO, Comment>(comment));
            UnitOfWork.Save();
        }

        /// <summary>
        /// Edits comment and set edit date and time to UTC now.
        /// </summary>
        /// <param name="comment"></param>
        public void EditComment(CommentDTO comment)
        {
            var commentEntity = mapper.Map<CommentDTO, Comment>(comment);

            commentEntity.EditDate = DateTime.UtcNow;

            UnitOfWork.Comments.EditComment(commentEntity);
            UnitOfWork.Save();
        }

        public void DeleteComment(Guid id)
        {
            UnitOfWork.Comments.Remove(id);
            UnitOfWork.Save();
        }
    }
}