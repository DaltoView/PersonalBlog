using AutoMapper;
using BLL.Interfaces;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.CommentsController;
using WebApi.Models.PostsController;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
            _mapper = AutomapperConfigs.AutomapperConfigs.GetCommentsControllerMapper();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateComment([FromBody]CommentModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentDto = _mapper.Map<CommentModel, CommentDTO>(comment);
            commentDto.AuthorId = new Guid(User.Identity.GetUserId());
            _commentService.CreateComment(commentDto);

            return Ok();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult EditComment([FromBody]CommentEditModel comment, [FromUri]Guid id)
        {
            var commentDto = _mapper.Map<CommentEditModel, CommentDTO>(comment);
            commentDto.Id = id;

            _commentService.EditComment(commentDto);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult DeleteComment(Guid id)
        {
            _commentService.DeleteComment(id);
            return Ok();
        }
    }
}