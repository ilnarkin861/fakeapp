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
    /// Handler-класс, вызывающий метод, который получает список постов для текущего авторизованного юзера
    /// </summary>
    public class UserPostsListHandler : PostHandler, IRequestHandler<UserPostsListQuery, PaginationResponse>
    {
        
        public UserPostsListHandler(IPostService postService, 
            IHttpService httpService) : base(postService, httpService)
        {
        }
        

        public async Task<PaginationResponse> Handle(UserPostsListQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await PostService.GetUserPostsListAsync(request.Offset, request.Limit,
               HttpService.GetHttpContext());

            foreach (var data in result.Data)
            {
                var post = (UserPostResponse) data;

                if (!string.IsNullOrEmpty(post.Image))
                {
                    post.Image = $"{HttpService.GetBaseUrl()}/{post.Image}";
                }
            }
            
            return result;
        }
    }
}