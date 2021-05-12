using Application.V1.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    [Authorize(Policy = "Admin")]
    public class UsersController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser.Command user)
        {
            var result = await Mediator.Send(user);

            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser.Command user)
        {
            var result = await Mediator.Send(user);

            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshUserToken.Command tokenRequest)
        {
            var result = await Mediator.Send(tokenRequest);

            return result.IsSuccessful
                ? Ok(result)
                : Unauthorized(result);
        }
    }
}
