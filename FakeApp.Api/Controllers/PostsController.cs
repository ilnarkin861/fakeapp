using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using FakeApp.Api.Hubs;
using FakeApp.Application.Posts.Commands;
using FakeApp.Application.Posts.Queries;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы со страницы постов в клиентском приложении
    /// </summary>
    public class PostsController : BaseController
    {
        private readonly IHubContext<CommentsHub> _hub;
        

        public PostsController(IHubContext<CommentsHub> hub)
        {
            _hub = hub;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPostsList([FromQuery] PostsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPostDetails(int id)
        {
            return Ok(await Mediator.Send(new PostDetailsQuery {PostId = id}));
        }


        [HttpGet]
        [Route("{id:int}/comments")]
        public async Task<IActionResult> GetPostComments(int id, [FromQuery] int offset, 
            [FromQuery] int limit)
        {
            return Ok(await Mediator
                .Send(new CommentsListQuery {PostId = id, Offset = offset, Limit = limit}));
        }


        [HttpPost]
        [Route("comments/add")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentAddCommand request)
        {
            var comment = await Mediator.Send(request);
            
            await _hub.Clients.All.SendAsync("Notify", comment);
            
            return Ok();
        }
    }
}