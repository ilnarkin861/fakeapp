using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;
using FakeApp.Application.Albums.Commands;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который удаляет альбом
    /// </summary>
    public class AlbumDeleteHandler : AlbumHandler, IRequestHandler<AlbumDeleteCommand, bool>
    {
        public AlbumDeleteHandler(IAlbumService albumService,
            IHttpService httpService) : base(albumService, httpService)
        {
        }

        
        public async Task<bool> Handle(AlbumDeleteCommand request, CancellationToken cancellationToken)
        {
            return await AlbumService.DeleteAlbumAsync(request.AlbumId, HttpService.GetHttpContext());
        }
    }
}