﻿using Application.V1.Models.Commands;
using Application.V1.Models.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
    [Authorize(Policy = "Admin")]
    public class ModelsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet("by-brandname/{brandName}")]
        public async Task<IActionResult> GetByBrandName(string brandName)
        {
            var models = await Mediator.Send(new GetModelsByBrandName.Query(brandName));

            return models is not null
                ? Ok(models)
                : NotFound();
        }

        [AllowAnonymous]
        [HttpGet("by-brandid/{brandId}")]
        public async Task<IActionResult> GetByBrandId(int brandId)
        {
            var models = await Mediator.Send(new GetModelsByBrandId.Query(brandId));

            return models is not null
                ? Ok(models)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateModel.Command model)
        {
            var createdModel = await Mediator.Send(model);

            return createdModel.IsSuccessful
                ? Ok(createdModel)
                : BadRequest(createdModel.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> EditById([FromBody] EditModel.Command model)
        {
            var editedModel = await Mediator.Send(model);

            return editedModel is not null
                ? Ok(editedModel)
                : BadRequest();
        }
    }
}
