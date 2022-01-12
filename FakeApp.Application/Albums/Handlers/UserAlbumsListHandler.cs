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
    /// Handler-класс, вызывающий метод, который получает список альбомов
    /// для текущего авторизованного юзера
    /// </summary>
    public class UserAlbumsListHandler : AlbumHandler, IRequestHandler<UserAlbumsListQuery, PaginationResponse>
    {
        public UserAlbumsListHandler(IAlbumService albumService,
            IHttpService httpService) : base(albumService, httpService)
        {
        }

        
        public async Task<PaginationResponse> Handle(UserAlbumsListQuery request,
            CancellationToken cancellationToken)
        {
            var result = await AlbumService
                .GetUserAlbumsListAsync(request.Offset, request.Limit, HttpService.GetHttpContext());

            foreach (var data in result.Data)
            {
                var album = (UserAlbumResponse) data;

                if (!string.IsNullOrEmpty(album.Cover))
                {
                    album.Cover = $"{HttpService.GetBaseUrl()}/{album.Cover}";
                }
            }

            return result;
        }
    }
}