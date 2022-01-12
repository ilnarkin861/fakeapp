using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка фоток в альбоме
    /// </summary>
    public class PhotosListQuery : IRequest<PaginationResponse>
    {
        public int AlbumId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}