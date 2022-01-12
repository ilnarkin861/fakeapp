using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Posts.Queries;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает список постов
    /// </summary>
    public class PostsListHandler : PostHandler, IRequestHandler<PostsListQuery, PaginationResponse>
    {
        public PostsListHandler(IPostService postService, 
            IHttpService httpService) : base(postService, httpService)
        {
        }

        
        public async Task<PaginationResponse> Handle(PostsListQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await PostService.GetPostsListAsync(request.Offset, request.Limit, request.UserId);

            foreach (var data in result.Data)
            {
                var post = (PostResponse) data;

                if (!string.IsNullOrEmpty(post.Image))
                {
                    post.Image = $"{HttpService.GetBaseUrl()}/{post.Image}";
                }

                if (!string.IsNullOrEmpty(post.UserAvatar))
                {
                    post.UserAvatar = $"{HttpService.GetBaseUrl()}/{post.UserAvatar}";
                }
            }
            
            return result;
        }
    }
}