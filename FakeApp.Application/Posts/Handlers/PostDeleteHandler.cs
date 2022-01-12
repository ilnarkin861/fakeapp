using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Posts.Commands;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который удаляет коммент
    /// </summary>
    public class PostDeleteHandler : PostHandler, IRequestHandler<PostDeleteCommand, bool>
    {
        public PostDeleteHandler(IPostService postService,
            IHttpService httpService) : base(postService, httpService)
        {
        }
        

        public async Task<bool> Handle(PostDeleteCommand request, CancellationToken cancellationToken)
        {
            return await PostService.DeletePostAsync(request.PostId, HttpService.GetHttpContext());
        }
    }
}