using MediatR;



namespace FakeApp.Application.Albums.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения конкретного альбома
    /// </summary>
    public class AlbumDetailsQuery : IRequest<AlbumResponse>
    {
        public int Id { get; set; }
    }
}