using BLL.DTO;
using BLL.DTO.Filters;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IPostService : IDisposable
    {
        IEnumerable<PostDTO> GetAllPosts(PostFilterDTO postFilterDTO);
        PostDTO GetPostById(Guid id);
        void CreatePost(PostDTO post);
        void EditPost(PostDTO post);
        void DeletePost(Guid id);
    }
}
