using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Posts.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка постов
    /// </summary>
    public class PostsListQuery : IRequest<PaginationResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int UserId { get; set; }
    }
}