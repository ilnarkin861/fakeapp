using MediatR;



namespace FakeApp.Application.Posts.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос удаления поста
    /// </summary>
    public class PostDeleteCommand : IRequest<bool>
    {
        public int PostId { get; set; }
    }
}