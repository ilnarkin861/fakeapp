using Microsoft.AspNetCore.Http;
using MediatR;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос добавления альбома
    /// </summary>
    public class AlbumAddCommand : IRequest<UserAlbumResponse>
    {
        public string Title { get; set; }
        public IFormFile Cover { get; set; }
    }
}