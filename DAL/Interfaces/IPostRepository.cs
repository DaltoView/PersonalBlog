using DAL.Entities;
using DAL.Entities.Filters;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        void CreatePost(Post post);
        IEnumerable<Post> GetAllPosts(PostFilter postFilter);
        void EditPost(Post post);
    }
}
