using MediatR;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос удаления фотки
    /// </summary>
    public class PhotoDeleteCommand : IRequest<bool>
    {
        public int AlbumId { get; set; }
        public int PhotoId { get; set; }
    }
}
