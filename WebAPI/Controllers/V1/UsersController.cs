using Application.V1.Users.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser.Command user)
        {
            var result = await Mediator.Send(user);

            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
