using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Service.Authentication.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LangBuddy.Authentication.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest authCreateRequest)
        {
            try
            {
                var res = await _authenticationService.Register(authCreateRequest);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest authLoginRequest)
        {
            try
            {
                var res = await _authenticationService.Authenticate(authLoginRequest);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRefreshRequest tokenRefreshRequest)
        {
            var email = User.Identity.Name;
            try
            {
                var res = await _authenticationService.RefreshToken(tokenRefreshRequest, email);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var email = User.Identity.Name;
            try
            {
                var res = await _authenticationService.Logout(email);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
