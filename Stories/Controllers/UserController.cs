using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stories.API.Helper;
using Stories.Domain;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Stories.API.Controllers
{

    [Route("stories-api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
    }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Stories.Domain.User user)
        {
            await this._userService.Authenticate(user.Username!, user.Password!);
            var token = TokenizationHelper.CreateToken(user.Username!, user.Password!);

            return Ok(token);
        }
    }
}
