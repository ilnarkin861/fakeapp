using Microsoft.AspNetCore.Http;
using MediatR;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос обновления альбома
    /// </summary>
    public class AlbumUpdateCommand : IRequest<UserAlbumResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile Cover { get; set; }
    }
}