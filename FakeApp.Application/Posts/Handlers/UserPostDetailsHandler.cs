using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Posts.Queries;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает конкретный пост для текущего авторизованного юзера
    /// </summary>
    public class UserPostDetailsHandler : PostHandler, IRequestHandler<UserPostDetailsQuery, UserPostResponse>
    {
        public UserPostDetailsHandler(IPostService postService,
            IHttpService httpService) : base(postService, httpService)
        {
        }
        

        public async Task<UserPostResponse> Handle(UserPostDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            var post = await PostService.GetUserPostAsync(request.PostId, HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(post.Image))
            {
                post.Image = $"{HttpService.GetBaseUrl()}/{post.Image}";
            }

            return post;
        }
    }
}