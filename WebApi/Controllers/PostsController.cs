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
using WebApi.Models.CommentsController;
using WebApi.Models.PostsController;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
            _mapper = AutomapperConfigs.AutomapperConfigs.GetPostsControllerMapper();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllPosts([FromUri]IEnumerable<string> tags = null, string search = null,
            DateTime? postFrom = null, DateTime? postTo = null, string sortOrder = null, int? offset = null, int? limit = 20)
        {
            var filter = new PostFilterDTO()
            {
                Tags = tags,
                Search = search,
                PostFrom = postFrom,
                PostTo = postTo,
                SortOrder = sortOrder,
                Skip = offset,
                Take = limit
            };

            try
            {
                var posts = _postService.GetAllPosts(filter);

                if (posts == null || !posts.Any())
                    return NotFound();

                return Ok(_mapper.Map<IEnumerable<PostDTO>, IEnumerable<PostModel>>(posts));
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetPostById(Guid id)
        {
            try
            {
                var post = _postService.GetPostById(id);

                if (post == null)
                    return NotFound();

                return Ok(_mapper.Map<PostDTO, PostModel>(post));
            }
            catch(Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreatePost(PostCreateModel post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var postDto = _mapper.Map<PostCreateModel, PostDTO>(post);
                postDto.AuthorId = new Guid(User.Identity.GetUserId());
                _postService.CreatePost(postDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult EditPost([FromBody]PostCreateModel post, [FromUri]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var postDto = _mapper.Map<PostCreateModel, PostDTO>(post);
                postDto.Id = id;
                _postService.EditPost(postDto);
            }
            catch(Exception)
            {
                return Conflict();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult DeletePost(Guid id)
        {
            try
            {
                _postService.DeletePost(id);
            }
            catch(Exception)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("{id:guid}/comments")]
        public IHttpActionResult GetAllCommentsByPostId(Guid id)
        {
            try
            {
                var comments = _commentService.GetCommentsByPostId(id);

                if (comments == null || !comments.Any())
                    return NotFound();

                return Ok(_mapper.Map<IEnumerable<CommentDTO>, IEnumerable<CommentModel>>(comments));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("{id:guid}/comments")]
        public IHttpActionResult CreateComment([FromBody]CommentModel comment, [FromUri]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var commentDto = _mapper.Map<CommentModel, CommentDTO>(comment);
                commentDto.AuthorId = new Guid(User.Identity.GetUserId());
                commentDto.PostId = id;
                _commentService.CreateComment(commentDto);
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            _postService.Dispose();
            _commentService.Dispose();
            base.Dispose(disposing);
        }
    }
}