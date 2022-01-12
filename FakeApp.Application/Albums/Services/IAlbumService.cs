using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Services
{
    /// <summary>
    /// Интерфейс для классов, работающих с альбомами
    /// </summary>
    public interface IAlbumService
    {
        Task<PaginationResponse> GetAlbumsListAsync(int offset, int limit, int userId);
        Task<AlbumResponse> GetAlbumByIdAsync(int id);
        Task<PaginationResponse> GetUserAlbumsListAsync(int offset, int limit, HttpContext httpContext);
        Task<UserAlbumResponse> GetUserAlbumAsync(int albumId, HttpContext httpContext);
        Task<UserAlbumResponse> AddAlbumAsync(Album album, HttpContext httpContext);
        Task<UserAlbumResponse> UpdateAlbumAsync(Album album, HttpContext httpContext);
        Task<bool> DeleteAlbumAsync(int albumId, HttpContext httpContext);
    }
}