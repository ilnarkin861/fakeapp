using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка альбомов
    /// </summary>
    public class AlbumsListQuery : IRequest<PaginationResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int UserId { get; set; }
    }
}