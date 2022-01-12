using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeApp.Application.Account.Queries;
using FakeApp.Application.Account.Commands;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы со страницы аккаунта юзера в клиентском приложении
    /// </summary>
    [Authorize]
    public class AccountController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAccountInfo()
        {
            return Ok(await Mediator.Send(new AccountDetailsQuery()));
        }


        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAccount([FromForm] AccountUpdateCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeCommand request)
        {
            await Mediator.Send(request);
            
            return Ok(new DefaultResponse
            {
                Message = "Password changed successfully",
                StatusCode = (int) HttpStatusCode.OK
            });
        }
    }
}