﻿using Application.V1.Brands.Commands;
using Application.V1.Brands.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    [Authorize(Policy = "Admin")]
    public class BrandsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await Mediator.Send(new GetBrands.Query());

            return brands is not null || brands.Any()
                ? Ok(brands)
                : NotFound();
        }

        [AllowAnonymous]
        [HttpGet("by-name/{brandName}")]
        public async Task<IActionResult> GetByName(string brandName)
        {
            var brand = await Mediator.Send(new GetBrandByName.Query(brandName));

            return brand is not null
                ? Ok(brand)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrand.Command brand)
        {
            var createdBrand = await Mediator.Send(brand);

            return createdBrand.IsSuccessful
                ? Ok(createdBrand)
                : BadRequest(createdBrand.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> EditById([FromBody] EditBrand.Command brand)
        {
            var editedBrand = await Mediator.Send(brand);

            return editedBrand.IsSuccessful
                ? Ok(editedBrand)
                : BadRequest(editedBrand.Errors);
        }
    }
}
