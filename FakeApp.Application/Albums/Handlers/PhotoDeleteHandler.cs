using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Albums.Commands;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который удаляет фотку
    /// </summary>
    public class PhotoDeleteHandler : PhotoHandler, IRequestHandler<PhotoDeleteCommand, bool>
    {
        public PhotoDeleteHandler(IPhotosService photosService,
            IHttpService httpService) : base(photosService, httpService)
        {
        }
        

        public async Task<bool> Handle(PhotoDeleteCommand request, CancellationToken cancellationToken)
        {
            return await PhotosService
                .DeletePhotoAsync(request.AlbumId, request.PhotoId, HttpService.GetHttpContext());
        }
    }
}