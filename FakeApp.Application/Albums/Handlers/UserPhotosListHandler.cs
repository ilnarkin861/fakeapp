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
    /// для текущего авторизованного юзера
    /// </summary>
    public class UserPhotosListHandler : PhotoHandler, 
        IRequestHandler<UserPhotosListQuery, PaginationResponse>
    {
        public UserPhotosListHandler(IPhotosService photosService,
            IHttpService httpService) : base(photosService, httpService)
        {
        }

        
        public async Task<PaginationResponse> Handle(UserPhotosListQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await PhotosService
                .GetPhotosAsync(request.AlbumId, request.Offset, request.Limit, HttpService.GetHttpContext());

            foreach (var data in result.Data)
            {
                var photo = (UserPhotosResponse) data;

                if (!string.IsNullOrEmpty(photo.Url))
                {
                    photo.Url = $"{HttpService.GetBaseUrl()}/{photo.Url}";
                }
            }

            return result;
        }
    }
}