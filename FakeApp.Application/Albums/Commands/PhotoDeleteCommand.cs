using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос удаления фотки
    /// </summary>
    public class PhotoDeleteCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Album id is required")]
        public int AlbumId { get; set; }
        
        [Required(ErrorMessage = "Photo id is required")]
        public int PhotoId { get; set; }
    }
}