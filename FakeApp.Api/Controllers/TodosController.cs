using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeApp.Application.Todos.Commands;
using FakeApp.Application.Todos.Queries;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы задач юзера в клиентском приложении
    /// </summary>
    [Authorize]
    public class TodosController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTodosList([FromQuery] TodosListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddTodo([FromBody] TodoAddCommand request)
        {
            return Created("todos/add", await Mediator.Send(request));
        }


        [HttpPut]
        [Route("change-status")]
        public async Task<IActionResult> ChangeTodoStatus([FromBody] TodoChangeStatusCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await Mediator.Send(new TodoDeleteCommand{TodoId = id});
            
            var response = new DefaultResponse
            {
                Message = "Todo deleted successfully", 
                StatusCode = (int) HttpStatusCode.OK
            };

            return Ok(response);
        }
    }
}