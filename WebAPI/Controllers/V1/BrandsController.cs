using Application.V1.Brands.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class BrandsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await Mediator.Send(new GetBrands.Query());

            return brands is not null || brands.Any()
                ? Ok(brands)
                : NotFound();
        }

        [HttpGet("by-name/{brandName}")]
        public async Task<IActionResult> GetByName(string brandName)
        {
            var brand = await Mediator.Send(new GetBrandByName.Query(brandName));

            return brand is not null
                ? Ok(brand)
                : NotFound();
        }
    }
}
