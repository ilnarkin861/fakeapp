using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Albums.Services
{
    /// <summary>
    /// Интерфейс для классов, работающих с фотками
    /// </summary>
    public interface IPhotosService
    {
        Task<PaginationResponse> GetPhotosAsync(int albumId, int offset, int limit);
        Task<PaginationResponse> GetPhotosAsync(int albumId, int offset, int limit, HttpContext httpContext);
        Task<ICollection<UserPhotosResponse>> UploadPhotosAsync(int albumId, ICollection<Photo> photos, HttpContext httpContext);
        Task<bool> DeletePhotoAsync(int albumId, int photoId, HttpContext httpContext);
    }
}