using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Queries.GetBrandList;
using Application.Features.Brands.Queries.GetBrandListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody]CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto createdBrandDto = await Mediator.Send(createBrandCommand);
            return Created("", createdBrandDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands([FromQuery]PageRequest pageRequest)
        {
            GetBrandListQuery getBrandListQuery = new GetBrandListQuery() { PageRequest = pageRequest };
            var result = await Mediator.Send(getBrandListQuery);
            return Ok(result);
        }
        [HttpPost("GetAll/ByDynamic")]
        public async Task<IActionResult> GetAllByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic? dynamic = null)
        {
            GetBrandListByDynamicQuery getBrandListByDynamicQuery = new() { Dynamic = dynamic, PageRequest = pageRequest };
            var result = await Mediator.Send(getBrandListByDynamicQuery);
            return Ok(result);
        }
    }
}
