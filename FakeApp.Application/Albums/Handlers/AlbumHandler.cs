using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Базовый handler-класс для альбомов
    /// </summary>
    public abstract class AlbumHandler
    {
        protected readonly IAlbumService AlbumService;
        protected readonly IHttpService HttpService;

        
        protected AlbumHandler(IAlbumService albumService, IHttpService httpService)
        {
            AlbumService = albumService;
            HttpService = httpService;
        }


        protected async Task<string> UploadAlbumCover(IFileSystemService fileSystemService, IFormFile cover)
        {
            var options = fileSystemService.GetMediaOptions();

            var albumCoversDir = Path.Combine(options.ImagesDir, options.AlbumCoversDir);
            
            var uploadDir = fileSystemService.CreateDir(albumCoversDir);

            var imageName = await fileSystemService
                .UploadFileAsync(Path.Combine(albumCoversDir, uploadDir), cover);

            return Path.Combine(albumCoversDir, uploadDir, imageName);
        }
    }
}