using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.AccountController;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            _mapper = AutomapperConfigs.AutomapperConfigs.GetAccountControllerMapper();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IHttpActionResult RegisterAccount([FromBody]UserRegisterModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDTO userDTO = _mapper.Map<UserRegisterModel, UserDTO>(user);

            try
            {
                OperationDetails result = _accountService.RegisterUser(userDTO);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAccount()
        {
            return Ok(_mapper.Map<UserDTO, UserAccountModel>(_accountService.GetUserById(new Guid(User.Identity.GetUserId()))));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult EditAccount([FromBody]UserAccountModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserAccountModel, UserDTO>(account);
            user.Id = new Guid(User.Identity.GetUserId());
            return Ok(_accountService.EditUser(user).Message);
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteAccount()
        {
            _accountService.DeleteUser(new Guid(User.Identity.GetUserId()));
            return Ok();
        }

        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok(User.Identity.GetUserId());
        }

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}