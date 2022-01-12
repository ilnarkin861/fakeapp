using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FakeApp.Api.Actions;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Базовый контроллер приложения
    /// </summary>
    [Controller]
    [Route("[controller]")]
    [ValidationAction]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}