using MediatR;
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

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    private IMediator? _mediator;
}