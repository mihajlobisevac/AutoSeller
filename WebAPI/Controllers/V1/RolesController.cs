using Application.V1.Roles.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    [Authorize(Policy = "Admin")]
    public class RolesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRole.Command role)
        {
            var result = await Mediator.Send(role);

            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("assign-user")]
        public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRole.Command role)
        {
            var result = await Mediator.Send(role);

            return result.IsSuccessful
                ? Ok(result)
                : BadRequest(result.Errors);
        }
    }
}
