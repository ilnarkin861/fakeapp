using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MediatR;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос обновления альбома
    /// </summary>
    public class AlbumUpdateCommand : IRequest<UserAlbumResponse>
    {
        [Required(ErrorMessage = "Album id is required")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Album title is required")]
        public string Title { get; set; }
        public IFormFile Cover { get; set; }
    }
}