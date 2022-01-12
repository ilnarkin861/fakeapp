using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FakeApp.Application.Posts.Commands;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который добавляет коммент к посту
    /// </summary>
    public class CommentAddHandler : CommentHandler, 
        IRequestHandler<CommentAddCommand, CommentResponse>
    {
        private readonly IMapper _mapper;
        
        
        public CommentAddHandler(ICommentService commentService, IHttpService httpService,
            IMapper mapper) : base(commentService, httpService)
        {
            _mapper = mapper;
        }
        

        public async Task<CommentResponse> Handle(CommentAddCommand request, 
            CancellationToken cancellationToken)
        {
            var comment = await CommentService
                .AddCommentAsync(_mapper.Map<Comment>(request), HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(comment.UserAvatar))
            {
                comment.UserAvatar = $"{HttpService.GetBaseUrl()}/{comment.UserAvatar}";
            }

            return comment;
        }
    }
}