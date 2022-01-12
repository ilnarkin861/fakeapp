using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FakeApp.Application.Users.Queries;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы со страницы юзеров в клиентском приложении
    /// </summary>
    public class UsersController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsersList([FromQuery] UsersListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            return Ok(await Mediator.Send(new UserDetailsQuery {UserId = id}));
        }
    }
}