using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrands;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//بسم الله الرحمن الرحيم
/**
 *
 * @author Huseyin_Aydin
 * @since 1994
 * @category DotNet Core nArchitechture, C#.
 *
 */

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        CreatedBrandDto result = await Mediator.Send(createBrandCommand);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
        BrandListModel result = await Mediator.Send(getListBrandQuery);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdIdBrandQuery)
    {
        BrandGetByIdDto brandGetByIdDto = await Mediator.Send(getByIdIdBrandQuery);
        return Ok(brandGetByIdDto);
    }
}