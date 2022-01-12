using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FakeApp.Application.Albums.Commands;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который обновляет альбом
    /// </summary>
    public class AlbumUpdateHandler : AlbumHandler, IRequestHandler<AlbumUpdateCommand, UserAlbumResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileSystemService _fileSystemService;


        public AlbumUpdateHandler(IAlbumService albumService,
            IMapper mapper, 
            IFileSystemService fileSystemService, 
            IHttpService httpService) : base(albumService, httpService)
        {
            _mapper = mapper;
            _fileSystemService = fileSystemService;
        }

        
        public async Task<UserAlbumResponse> Handle(AlbumUpdateCommand request, CancellationToken cancellationToken)
        {
            var album = _mapper.Map<Album>(request);

            if (request.Cover != null)
            {
                album.Cover = await UploadAlbumCover(_fileSystemService, request.Cover);
            }

            var result = await AlbumService.UpdateAlbumAsync(album, HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(result.Cover))
            {
                result.Cover = $"{HttpService.GetBaseUrl()}/{result.Cover}";
            }

            return result;
        }
    }
}