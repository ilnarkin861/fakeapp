using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Posts.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка постов для текущего авторизованного юзера
    /// </summary>
    public class UserPostsListQuery : IRequest<PaginationResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}