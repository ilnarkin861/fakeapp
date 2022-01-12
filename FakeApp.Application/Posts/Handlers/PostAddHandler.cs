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
    /// Handler-класс, вызывающий метод, который добавляет пост
    /// </summary>
    public class PostAddHandler : PostHandler, IRequestHandler<PostAddCommand, UserPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileSystemService _fileSystemService;


        public PostAddHandler(IPostService postService, 
            IHttpService httpService,
            IMapper mapper, 
            IFileSystemService fileSystemService) : base(postService, httpService)
        {
            _mapper = mapper;
            _fileSystemService = fileSystemService;
        }

        
        public async Task<UserPostResponse> Handle(PostAddCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);

            if (request.Image != null)
            {
                post.Image = await UploadPostImage(_fileSystemService, request.Image);
            }

            var result = await PostService.AddPostAsync(post, HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(result.Image))
            {
                result.Image = $"{HttpService.GetBaseUrl()}/{result.Image}";
            }

            return result;
        }
    }
}