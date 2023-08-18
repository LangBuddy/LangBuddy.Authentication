using LangBuddy.Authentication.Models.Request;
using LangBuddy.Authentication.Service.Authentication.Common;
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
    }
}
