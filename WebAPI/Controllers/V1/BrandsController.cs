using Application.V1.Brands.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    public class BrandsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<GetBrands.Response>> GetAll()
        {
            return await Mediator.Send(new GetBrands.Query());
        }
    }
}
