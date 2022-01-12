using MediatR;



namespace FakeApp.Application.Posts.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения конкретного поста
    /// </summary>
    public class PostDetailsQuery : IRequest<PostResponse>
    {
        public int PostId { get; set; }
    }
}