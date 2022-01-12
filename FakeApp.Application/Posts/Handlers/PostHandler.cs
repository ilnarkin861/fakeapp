using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Application.Posts.Handlers
{
    /// <summary>
    /// Базовый handler-класс для постов
    /// </summary>
    public abstract class PostHandler
    {
        protected readonly IPostService PostService;
        protected readonly IHttpService HttpService;

        
        protected PostHandler(IPostService postService, IHttpService httpService)
        {
            PostService = postService;
            HttpService = httpService;
        }


        protected async Task<string> UploadPostImage(IFileSystemService fileSystemService, IFormFile image)
        {
            var options = fileSystemService.GetMediaOptions();

            var postImagesDir = Path.Combine(options.ImagesDir, options.PostImagesDir);

            var uploadDir = fileSystemService.CreateDir(postImagesDir);

            var imageName = await fileSystemService
                .UploadFileAsync(Path.Combine(postImagesDir, uploadDir), image);

            return Path.Combine(postImagesDir, uploadDir, imageName);
        }
    }
}