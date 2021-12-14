using MHA.Core.Contracts.Services;
using MHA.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MHA.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult Register(NewUserDTO newUser)
        {
            try
            {
                _userService.InsertUser(newUser);
                return Ok("User added correctly");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(UserCredentialsDTO userCredentials)
        {
            try
            {
                return await _userService.AuthenticateUser(userCredentials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
