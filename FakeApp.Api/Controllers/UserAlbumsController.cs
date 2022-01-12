using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FakeApp.Application.Albums.Commands;
using FakeApp.Application.Albums.Queries;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Api.Controllers
{
    /// <summary>
    /// Контроллер на запросы альбомов юзера в клиентском приложении
    /// </summary>
    [Authorize]
    public class UserAlbumsController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUserAlbumsList([FromQuery] UserAlbumsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserAlbumsDetails(int id)
        {
            return Ok(await Mediator.Send(new UserAlbumDetailsQuery {Id = id}));
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAlbum([FromForm] AlbumAddCommand request)
        {
            return Created("useralbums/add", await Mediator.Send(request));
        }


        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditAlbum([FromForm] AlbumUpdateCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            await Mediator.Send(new AlbumDeleteCommand {AlbumId = id});

            var response = new DefaultResponse
            {
                Message = "Album deleted successfully", 
                StatusCode = (int) HttpStatusCode.OK
            };

            return Ok(response);
        }
        
        
        [HttpGet]
        [Route("{id:int}/photos")]
        public async Task<IActionResult> GetPhotos(int id, [FromQuery] int offset, [FromQuery] int limit)
        {
            return Ok(await Mediator
                .Send(new UserPhotosListQuery{AlbumId = id, Offset = offset, Limit = limit}));
        }


        [HttpPost]
        [Route("photos/upload")]
        public async Task<IActionResult> UploadPhotos([FromForm] PhotosUploadCommand request)
        {
            return Created("useralbums/photos/upload", await Mediator.Send(request));
        }


        [HttpDelete]
        [Route("{albumId:int}/photos/delete/{photoId:int}")]
        public async Task<IActionResult> DeletePhoto(int albumId, int photoId)
        {
            await Mediator.Send(new PhotoDeleteCommand {AlbumId = albumId, PhotoId = photoId});

            var response = new DefaultResponse
            {
                Message = "Photo deleted successfully", 
                StatusCode = (int) HttpStatusCode.OK
            };
            
            return Ok(response);
        }
    }
}