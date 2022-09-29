using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Queries.GetBrandList;
using Core.Application.Requests;
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
    }
}
