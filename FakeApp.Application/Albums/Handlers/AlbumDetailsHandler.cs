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
    /// </summary>
    public class AlbumDetailsHandler : AlbumHandler, IRequestHandler<AlbumDetailsQuery, AlbumResponse>
    {
        public AlbumDetailsHandler(IAlbumService albumService, 
            IHttpService httpService) : base(albumService, httpService)
        {
        }
        

        public async Task<AlbumResponse> Handle(AlbumDetailsQuery request, CancellationToken cancellationToken)
        {
            var album = await AlbumService.GetAlbumByIdAsync(request.Id);

            if (!string.IsNullOrEmpty(album.Cover))
            {
                album.Cover = $"{HttpService.GetBaseUrl()}/{album.Cover}";
            }
            
            return album;
        }
    }
}