using BLL.DTO;
using BLL.DTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAllPosts(PostFilterDTO postFilterDTO);
        PostDTO GetPostById(Guid id);
        void CreatePost(PostDTO post);
        void EditPost(PostDTO post);
        void DeletePost(Guid id);
    }
}
