using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Базовый handler-класс для комментов
    /// </summary>
    public abstract class CommentHandler
    {
        protected readonly ICommentService CommentService;
        protected readonly IHttpService HttpService;

        
        protected CommentHandler(ICommentService commentService, IHttpService httpService)
        {
            CommentService = commentService;
            HttpService = httpService;
        }
    }
}