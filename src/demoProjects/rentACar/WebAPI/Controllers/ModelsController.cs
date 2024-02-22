using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModel;
using Application.Features.Models.Queries.GetListModelByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
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
public class ModelsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
        ModelListModel result = await Mediator.Send(getListModelQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
    {
        GetListModelByDynamicQuery getListModelByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic  };
        ModelListModel modelListModel = await Mediator.Send(getListModelByDynamicQuery);
        return Ok(modelListModel);
    }
}