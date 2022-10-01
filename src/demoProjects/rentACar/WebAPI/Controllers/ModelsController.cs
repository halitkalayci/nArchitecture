using Application.Features.Brands.Queries.GetBrandListByDynamic;
using Application.Features.Models.Commands.CreateModel;
using Application.Features.Models.Queries.GetModelListWithBrand;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWithBrand([FromQuery] PageRequest pageRequest)
        {
            GetModelListWithBrandQuery query = new() { PageRequest = pageRequest };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddNew([FromBody] CreateModelCommand createModelCommand)
        {
            var result = await Mediator.Send(createModelCommand);
            return Created("", result);
        }
       
    }
}
