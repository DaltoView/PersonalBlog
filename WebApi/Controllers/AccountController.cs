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
            
            try
            {
                UserDTO userDTO = _mapper.Map<UserRegisterModel, UserDTO>(user);
                OperationDetails result = _accountService.RegisterUser(userDTO);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return StatusCode(HttpStatusCode.NoContent);
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
            try
            {
                var user = _accountService.GetUserById(new Guid(User.Identity.GetUserId()));

                if (user == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDTO, UserAccountModel>(user));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult EditAccount([FromBody]UserAccountModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<UserAccountModel, UserDTO>(account);
                user.Id = new Guid(User.Identity.GetUserId());
                var result = _accountService.EditUser(user);

                if (!result.Succeeded)
                    return Conflict();

                return Ok();
            }
            catch(Exception)
            {
                return Conflict();
            }

        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteAccount()
        {
            try
            {
                var result = _accountService.DeleteUser(new Guid(User.Identity.GetUserId()));

                if (!result.Succeeded)
                    return NotFound();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}