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
    /// Handler-класс, вызывающий метод, который получает список комментов к посту
    /// </summary>
    public class CommentsListHandler : CommentHandler, IRequestHandler<CommentsListQuery, PaginationResponse>
    {
        public CommentsListHandler(ICommentService commentService, 
            IHttpService httpService) : base(commentService, httpService)
        {
        }

        
        public async Task<PaginationResponse> Handle(CommentsListQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await CommentService
                .GetCommentsListAsync(request.PostId, request.Offset, request.Limit);

            foreach (var data in result.Data)
            {
                var comment = (CommentResponse) data;

                if (!string.IsNullOrEmpty(comment.UserAvatar))
                {
                    comment.UserAvatar = $"{HttpService.GetBaseUrl()}/{comment.UserAvatar}";
                }
            }

            return result;
        }
    }
}