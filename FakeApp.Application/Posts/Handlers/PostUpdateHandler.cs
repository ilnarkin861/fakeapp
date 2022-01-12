using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FakeApp.Application.Posts.Commands;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который обновляет пост
    /// </summary>
    public class PostUpdateHandler : PostHandler, IRequestHandler<PostUpdateCommand, UserPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileSystemService _fileSystemService;


        public PostUpdateHandler(IPostService postService,
            IMapper mapper, 
            IFileSystemService fileSystemService,
            IHttpService httpService) : base(postService, httpService)
        {
            _mapper = mapper;
            _fileSystemService = fileSystemService;
        }
        

        public async Task<UserPostResponse> Handle(PostUpdateCommand request, 
            CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
                
            if (request.Image != null)
            {
                post.Image = await UploadPostImage(_fileSystemService, request.Image);
            }
                
            var result = await PostService.UpdatePostAsync(post, HttpService.GetHttpContext());
            
            if (!string.IsNullOrEmpty(result.Image))
            {
                result.Image = $"{HttpService.GetBaseUrl()}/{result.Image}";
            }

            return result;
        }
    }
}