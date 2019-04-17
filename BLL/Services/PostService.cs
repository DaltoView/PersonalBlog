using BLL.DTO;
using BLL.DTO.Filters;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Entities.Filters;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for performing actions on posts.
    /// </summary>
    public class PostService : BaseService<PostDTO, Post>, IPostService
    {
        public PostService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            mapper = AutomapperConfigs.AutomapperConfigs.GetPostServiceMapper();
        }

        /// <summary>
        ///  Gets all posts by filter parameters.
        /// </summary>
        /// <param name="postFilterDTO"></param>
        /// <returns></returns>
        public IEnumerable<PostDTO> GetAllPosts(PostFilterDTO postFilterDTO)
        {
            var posts = UnitOfWork.Posts.GetAllPosts(mapper.Map<PostFilterDTO, PostFilter>(postFilterDTO));
            return mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);
        }

        public PostDTO GetPostById(Guid id)
        {
            var post = UnitOfWork.Posts.Get(id);
            return mapper.Map<Post, PostDTO>(post);
        }

        /// <summary>
        /// Creates post and set post date and time to UTC now and edit date and time to default ("1980-01-01").
        /// </summary>
        /// <param name="post"></param>
        public void CreatePost(PostDTO post)
        {
            post.PostDate = DateTime.UtcNow;
            post.EditDate = DateTime.Parse("1980-01-01");

            var postEntity = mapper.Map<PostDTO, Post>(post);

            UnitOfWork.Posts.CreatePost(postEntity);
            UnitOfWork.Save();
        }

        /// <summary>
        /// Edits post and set edit date and time to UTC now.
        /// </summary>
        /// <param name="post"></param>
        public void EditPost(PostDTO post)
        {
            post.EditDate = DateTime.UtcNow;

            var postEntity = mapper.Map<PostDTO, Post>(post);

            UnitOfWork.Posts.EditPost(postEntity);
            UnitOfWork.Save();
        }

        public void DeletePost(Guid id)
        {
            UnitOfWork.Posts.Remove(id);
            UnitOfWork.Save();
        }
    }
}