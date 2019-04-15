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
        public IHttpActionResult GetAll(UserFilterDTO userSearchDTO)
        {
            return Ok(_mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(_userService.GetAllUsers(userSearchDTO)));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetById(Guid id)
        {
            return Ok(_mapper.Map<UserDTO, UserModel>(_userService.GetUserById(id)));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateUser(UserCreateModel user)
        {
            var userDto = _mapper.Map<UserCreateModel, UserDTO>(user);
            return Ok(_userService.CreateUser(userDto));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult EditUser([FromBody]UserModel user, [FromUri]Guid id)
        {
            var userDto = _mapper.Map<UserModel, UserDTO>(user);
            userDto.Id = id;
            return Ok(_userService.EditUser(userDto));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult DeleteUser(Guid id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}