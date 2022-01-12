using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Albums.Queries;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает конкретный альбом
    /// для текущего авторизованного юзера
    /// </summary>
    public class UserAlbumDetailsHandler : AlbumHandler, IRequestHandler<UserAlbumDetailsQuery, UserAlbumResponse>
    {
        public UserAlbumDetailsHandler(IAlbumService albumService,
            IHttpService httpService) : base(albumService, httpService)
        {
            
        }

        
        public async Task<UserAlbumResponse> Handle(UserAlbumDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            var album = await AlbumService.GetUserAlbumAsync(request.Id, HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(album.Cover))
            {
                album.Cover = $"{HttpService.GetBaseUrl()}/{album.Cover}";
            }

            return album;
        }
    }
}