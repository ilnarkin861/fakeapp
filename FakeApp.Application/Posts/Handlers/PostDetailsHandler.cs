using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Posts.Queries;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает конкретный пост
    /// </summary>
    public class PostDetailsHandler : PostHandler, IRequestHandler<PostDetailsQuery, PostResponse>
    {
        public PostDetailsHandler(IPostService postService, 
            IHttpService httpService) : base(postService, httpService)
        {
        }
        

        public async Task<PostResponse> Handle(PostDetailsQuery request, CancellationToken cancellationToken)
        {
            var post = await PostService.GetPostByIdAsync(request.PostId);

            if (!string.IsNullOrEmpty(post.Image))
            {
                post.Image = $"{HttpService.GetBaseUrl()}/{post.Image}";
            }

            if (!string.IsNullOrEmpty(post.UserAvatar))
            {
                post.UserAvatar = $"{HttpService.GetBaseUrl()}/{post.UserAvatar}";
            }

            return post;
        }
    }
}