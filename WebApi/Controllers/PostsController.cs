using AutoMapper;
using BLL.DTO;
using BLL.DTO.Filters;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.PostsController;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService)
        {
            _postService = postService;
            _mapper = AutomapperConfigs.AutomapperConfigs.GetPostsControllerMapper();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllPosts(PostFilterDTO postFilterDTO)
        {
            return Ok(_mapper.Map<IEnumerable<PostDTO>, IEnumerable<PostModel>>(_postService.GetAllPosts(postFilterDTO)));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetPostById(Guid id)
        {
            return Ok(_mapper.Map<PostDTO, PostModel>(_postService.GetPostById(id)));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreatePost(PostCreateModel post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postDto = _mapper.Map<PostCreateModel, PostDTO>(post);
            postDto.AuthorId = new Guid(User.Identity.GetUserId());
            _postService.CreatePost(postDto);
            return Ok();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult EditPost([FromBody]PostCreateModel post, [FromUri]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postDto = _mapper.Map<PostCreateModel, PostDTO>(post);
            postDto.Id = id;
            _postService.EditPost(postDto);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult DeletePost(Guid id)
        {
            _postService.DeletePost(id);
            return Ok();
        }
    }
}