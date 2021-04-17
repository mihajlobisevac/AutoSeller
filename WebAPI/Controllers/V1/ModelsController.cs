using Application.V1.Models.Commands;
using Application.V1.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class ModelsController : ApiControllerBase
    {
        [HttpGet("by-brandname/{brandName}")]
        public async Task<IActionResult> GetByBrandName(string brandName)
        {
            var brands = await Mediator.Send(new GetModelsByBrandName.Query(brandName));

            return brands is not null
                ? Ok(brands)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateModel.Command model)
        {
            var createdModel = await Mediator.Send(model);

            return createdModel is not null
                ? Ok(createdModel)
                : BadRequest(createdModel);
        }
    }
}
