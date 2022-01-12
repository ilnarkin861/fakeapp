using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Helpers;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Albums.Services
{
    /// <summary>
    /// Класс, работающий с фотками
    /// </summary>
    public class PhotosService : BaseService, IPhotosService
    {
        public PhotosService(AppDbContext dbContext, IMapper mapper, 
            IUserManager userManager) : base(dbContext, mapper, userManager)
        {
        }
        
        
        public async Task<PaginationResponse> GetPhotosAsync(int albumId, int offset, int limit)
        {
            var photosLimit = limit is 0 or > Settings.PhotosLimit ? Settings.PhotosLimit : limit;

            var query = DbContext.Photos.AsNoTracking().Where(p => p.AlbumId == albumId);
            
            var count = await query.CountAsync();

            var photos = await query
                .OrderByDescending(p => p.Id)
                .Skip(offset)
                .Take(photosLimit)
                .ProjectTo<PhotosResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();
            
            return new PaginationResponse
                {
                    Data = photos,
                    Pagination = new Paginator(count, offset, photosLimit)
                };
        }
        

        public async Task<PaginationResponse> GetPhotosAsync(int albumId, int offset, int limit, 
            HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);
            
            var photosLimit = limit is 0 or > Settings.UserPhotosLimit ? Settings.UserPhotosLimit : limit;

            var query = DbContext.Photos.AsNoTracking().Where(p => p.AlbumId == albumId && p.Album.User == user);
            
            var count = await query.CountAsync();

            var photos = await query
                .OrderByDescending(p => p.Id)
                .Skip(offset)
                .Take(photosLimit)
                .ProjectTo<UserPhotosResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();
            
            return new PaginationResponse
                {
                    Data = photos,
                    Pagination = new Paginator(count, offset, photosLimit)
                };
        }
        

        public async Task<ICollection<UserPhotosResponse>> UploadPhotosAsync(int albumId, ICollection<Photo> photos,
            HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var albumExists = await DbContext.Albums.AnyAsync(a => a.Id == albumId && a.User == user);

            if (!albumExists)
            {
                throw new RestException("Not found album for upload photos");
            }
                
            foreach (var photo in photos)
            {
                photo.AlbumId = albumId;
            }
                
            await DbContext.AddRangeAsync(photos);

            await DbContext.SaveChangesAsync();

            return Mapper.Map<ICollection<Photo>, ICollection<UserPhotosResponse>>(photos);
        }
        

        public async Task<bool> DeletePhotoAsync(int albumId, int photoId, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var photo = await DbContext.Photos
                .Where(p => p.AlbumId == albumId && p.Id == photoId && p.Album.User == user)
                .FirstOrDefaultAsync();

            if (photo == null)
            {
                throw new RestException("Not found photo for delete");
            }

            DbContext.Remove(photo);

            await DbContext.SaveChangesAsync();

            return true;
        }
        
        
        private async Task<User> GetCurrentUser(HttpContext httpContext)
        {
            return await UserManager.GetCurrentUserAsync(httpContext);
        }
    }
}