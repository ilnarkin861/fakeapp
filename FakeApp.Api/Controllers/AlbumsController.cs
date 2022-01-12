using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FakeApp.Application.Albums.Queries;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы со страницы альбомов в клиентском приложении
    /// </summary>
    public class AlbumsController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAlbumsList([FromQuery] AlbumsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPostDetails(int id)
        {
            return Ok(await Mediator.Send(new AlbumDetailsQuery {Id = id}));
        }


        [HttpGet]
        [Route("{id:int}/photos")]
        public async Task<IActionResult> GetPhotos(int id, [FromQuery] int offset, [FromQuery] int limit)
        {
            return Ok(await Mediator.Send(new PhotosListQuery {AlbumId = id, Offset = offset, Limit = limit}));
        }
    }
}