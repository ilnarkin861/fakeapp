using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FakeApp.Api.Auth.Actions;
using FakeApp.Api.Auth.Commands;



namespace FakeApp.Api.Auth.Controllers
{
    /// <summary>
    /// Контроллер на запросы авторизации
    /// </summary>
    [Controller]
    [Route("[controller]")]
    [ValidationAction]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        private IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> DefaultSignIn([FromBody] BasicSignInCommand request)
        {
            return Ok(new {accessToken = await Mediator.Send(request)});
        }

        
        [HttpPost]
        [Route("social")]
        public async Task<IActionResult> SocialSignIn([FromBody] SocialSignInCommand request)
        {
            return Ok(new {accessToken = await Mediator.Send(request)});
        }


        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> BasicSignUp([FromBody] BasicSignUpCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}