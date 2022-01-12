using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeApp.Application.Posts.Commands;
using FakeApp.Application.Posts.Queries;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы постов юзера в клиентском приложении
    /// </summary>
    [Authorize]
    public class UserPostsController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUserPostsList([FromQuery] UserPostsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserPostDetails(int id)
        {
           return Ok(await Mediator.Send(new UserPostDetailsQuery {PostId = id}));
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPost([FromForm] PostAddCommand request)
        {
            return Created("userposts/add", await Mediator.Send(request));
        }


        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditPost([FromForm] PostUpdateCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await Mediator.Send(new PostDeleteCommand {PostId = id});

            var response = new DefaultResponse
            {
                Message = "Post deleted successfully", 
                StatusCode = (int) HttpStatusCode.OK
            };
            
            return Ok(response);
        }


        [HttpPost]
        [Route("comments/add")]
        public async Task<IActionResult> AddComment([FromBody] CommentAddCommand request)
        {
            return Created("userposts/comments/add", await Mediator.Send(request));
        }
    }
}