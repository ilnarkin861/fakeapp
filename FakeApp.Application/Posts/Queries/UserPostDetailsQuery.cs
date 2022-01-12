using MediatR;



namespace FakeApp.Application.Posts.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения конкретного поста для текущего авторизованного юзера
    /// </summary>
    public class UserPostDetailsQuery : IRequest<UserPostResponse>
    {
        public int PostId { get; set; }
    }
}