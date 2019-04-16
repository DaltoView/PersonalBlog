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

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult EditComment([FromBody]CommentEditModel comment, [FromUri]Guid id)
        {
            try
            {
                var commentDto = _mapper.Map<CommentEditModel, CommentDTO>(comment);
                commentDto.Id = id;
                _commentService.EditComment(commentDto);
            }
            catch (Exception)
            {
                return Conflict();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult DeleteComment(Guid id)
        {
            try
            {
                _commentService.DeleteComment(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            _commentService.Dispose();
            base.Dispose(disposing);
        }
    }
}