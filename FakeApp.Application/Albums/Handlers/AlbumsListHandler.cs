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
    /// </summary>
    public class AlbumsListHandler : AlbumHandler, IRequestHandler<AlbumsListQuery, PaginationResponse>
    {
        public AlbumsListHandler(IAlbumService albumService, 
            IHttpService httpService) : base(albumService, httpService)
        {
        }
        

        public async Task<PaginationResponse> Handle(AlbumsListQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await AlbumService.GetAlbumsListAsync(request.Offset, request.Limit, request.UserId);

            foreach (var data in result.Data)
            {
                var album = (AlbumResponse) data;

                if (!string.IsNullOrEmpty(album.Cover))
                {
                    album.Cover = $"{HttpService.GetBaseUrl()}/{album.Cover}";
                }
            }
            
            return result;
        }
    }
}