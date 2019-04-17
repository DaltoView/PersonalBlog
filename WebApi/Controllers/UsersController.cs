using AutoMapper;
using BLL.DTO;
using BLL.DTO.Filters;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.UsersController;

namespace WebApi.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService)
        {
            _userService = userService;
            _mapper = AutomapperConfigs.AutomapperConfigs.GetUsersControllerMapper();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll([FromUri] string search = null, string role = null, int? ageFrom = null, 
            int? ageTo = null, string sortOrder = null, int? offset = null, int? limit = 20)
        {
            var filter = new UserFilterDTO()
            {
                Search = search,
                Role = role,
                AgeFrom = ageFrom,
                AgeTo = ageTo,
                SortOrder = sortOrder,
                Skip = offset,
                Take = limit
            };

            try
            {
                var users = _userService.GetAllUsers(filter);

                if (users == null || !users.Any())
                    return NotFound();

                return Ok(_mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(users));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetById(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDTO, UserModel>(user));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateUser(UserCreateModel user)
        {
            try
            {
                var userDto = _mapper.Map<UserCreateModel, UserDTO>(user);
                _userService.CreateUser(userDto);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult EditUser([FromBody]UserModel user, [FromUri]Guid id)
        {
            try
            {
                var userDto = _mapper.Map<UserModel, UserDTO>(user);
                userDto.Id = id;
                _userService.EditUser(userDto);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception)
            {
                return Conflict();
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult DeleteUser(Guid id)
        {
            try
            {
                _userService.DeleteUser(id);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            base.Dispose(disposing);
        }
    }
}