using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Albums.Commands;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который загружает фотки
    /// </summary>
    public class PhotosUploadHandler : PhotoHandler, 
        IRequestHandler<PhotosUploadCommand, ICollection<UserPhotosResponse>>
    {
        private readonly IFileSystemService _fileSystemService;
        
        
        public PhotosUploadHandler(IPhotosService photosService, IHttpService httpService,
            IFileSystemService fileSystemService) : base(photosService, httpService)
        {
            _fileSystemService = fileSystemService;
        }

        public async Task<ICollection<UserPhotosResponse>> Handle(PhotosUploadCommand request,
            CancellationToken cancellationToken)
        {
            var photosList = new List<Photo>();

            var options = _fileSystemService.GetMediaOptions();

            var photosDir = Path.Combine(options.ImagesDir, options.PhotosDir);
                
            var newDir = _fileSystemService.CreateDir(photosDir);

            foreach (var photo in request.Photos)
            {
                var uploadedPhoto = await _fileSystemService
                    .UploadFileAsync(Path.Combine(photosDir, newDir), photo);
                    
                photosList.Add(new Photo{Url = Path.Combine(photosDir, newDir, uploadedPhoto)});
            }

            var uploadedPhotos = await PhotosService
                .UploadPhotosAsync(request.AlbumId, photosList, HttpService.GetHttpContext());

            foreach (var photo in uploadedPhotos)
            {
                if (!string.IsNullOrEmpty(photo.Url))
                {
                    photo.Url = $"{HttpService.GetBaseUrl()}/{photo.Url}";
                }
            }
            
            return uploadedPhotos;
        }
    }
}