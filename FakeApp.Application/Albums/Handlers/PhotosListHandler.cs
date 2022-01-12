using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Albums.Queries;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает список фоток в альбоме
    /// </summary>
    public class PhotosListHandler : PhotoHandler, IRequestHandler<PhotosListQuery, PaginationResponse>
    {
        public PhotosListHandler(IPhotosService photosService,
            IHttpService httpService) : base(photosService, httpService)
        {
        }

        
        public async Task<PaginationResponse> Handle(PhotosListQuery request, CancellationToken cancellationToken)
        {
            var result = await PhotosService.GetPhotosAsync(request.AlbumId, request.Offset, request.Limit);

            foreach (var data in result.Data)
            {
                var photo = (PhotosResponse) data;

                if (!string.IsNullOrEmpty(photo.Url))
                {
                    photo.Url = $"{HttpService.GetBaseUrl()}/{photo.Url}";
                }
            }
            
            return result;
        }

        
    }
}