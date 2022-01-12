using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос удаления альбома
    /// </summary>
    public class AlbumDeleteCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Album id is required")]
        public int AlbumId { get; set; }
    }
}