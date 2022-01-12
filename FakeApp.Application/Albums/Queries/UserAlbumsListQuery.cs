using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка альбомов для текущего
    /// авторизованного юзера
    /// </summary>
    public class UserAlbumsListQuery : IRequest<PaginationResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}