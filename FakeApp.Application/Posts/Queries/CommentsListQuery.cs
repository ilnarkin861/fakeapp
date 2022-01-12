using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Posts.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка комментов к посту
    /// </summary>
    public class CommentsListQuery : IRequest<PaginationResponse>
    {
        public int PostId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}