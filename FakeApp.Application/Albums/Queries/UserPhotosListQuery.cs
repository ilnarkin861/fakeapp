using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения фоток в альбоме для текущего
    /// авторизованного юзера
    /// </summary>
    public class UserPhotosListQuery : IRequest<PaginationResponse>
    {
        public int AlbumId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}