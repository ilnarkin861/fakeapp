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
    /// Класс, работающий с альбомами
    /// </summary>
    public class AlbumService : BaseService, IAlbumService
    {
        public AlbumService(AppDbContext dbContext, IMapper mapper, 
            IUserManager userManager) : base(dbContext, mapper, userManager)
        {
        }

        
        public async Task<PaginationResponse> GetAlbumsListAsync(int offset, int limit, int userId)
        {
            var albumsLimit = limit is 0 or > Settings.AlbumsLimit ? Settings.AlbumsLimit : limit;

            var query = DbContext.Albums.AsNoTracking();

            if (userId != 0)
            {
                query = query.Where(a => a.UserId == userId);
            }

            var count = await query.CountAsync();

            var albums = await query
                .OrderByDescending(a => a.Id)
                .Skip(offset)
                .Take(albumsLimit)
                .ProjectTo<AlbumResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginationResponse
                {
                    Data = albums,
                    Pagination = new Paginator(count, offset, albumsLimit)
                    
                };
        }
        

        public async Task<AlbumResponse> GetAlbumByIdAsync(int id)
        {
            var album = await DbContext.Albums
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Include(a => a.Photos)
                .AsSplitQuery()
                .ProjectTo<AlbumResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new EntityNotFoundException("Album not found");
            }

            return album;
        }

        
        public async Task<PaginationResponse> GetUserAlbumsListAsync(int offset, int limit, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var albumsLimit = limit is 0 or > Settings.UserAlbumsLimit ? Settings.UserAlbumsLimit : limit;

            var query = DbContext.Albums.AsNoTracking().Where(a => a.User == user);

            var count = await query.CountAsync();

            var albums = await query
                .OrderByDescending(a => a.Id)
                .Skip(offset)
                .Take(albumsLimit)
                .ProjectTo<UserAlbumResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();
            
            return new PaginationResponse
                {
                    Data = albums,
                    Pagination = new Paginator(count, offset, albumsLimit)
                };
        }
        

        public async Task<UserAlbumResponse> GetUserAlbumAsync(int albumId, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);
            
            var album = await DbContext.Albums
                .AsNoTracking()
                .Where(a => a.Id == albumId && a.User == user)
                .ProjectTo<UserAlbumResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new EntityNotFoundException("User album not found");
            }

            return album;
        }
        

        public async Task<UserAlbumResponse> AddAlbumAsync(Album album, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            album.UserId = user.Id;

            DbContext.Add(album);

            await DbContext.SaveChangesAsync();

            return Mapper.Map<UserAlbumResponse>(album);
        }
        

        public async Task<UserAlbumResponse> UpdateAlbumAsync(Album album, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var albumExists = await DbContext.Albums.AnyAsync(a => a.Id == album.Id && a.User == user);

            if (!albumExists)
            {
                throw new RestException("Not found album for update");
            }

            album.UserId = user.Id;
                
            album.Cover ??= await DbContext.Albums
                .Where(a => a.Id == album.Id)
                .Select(a => a.Cover)
                .FirstOrDefaultAsync();

            DbContext.Update(album);

            await DbContext.SaveChangesAsync();

            return Mapper.Map<UserAlbumResponse>(album);
        }
        

        public async Task<bool> DeleteAlbumAsync(int albumId, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);
                
            var album = await DbContext.Albums
                .Where(a => a.Id == albumId && a.User == user)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new RestException("Not found album for delete");
            }

            DbContext.Remove(album);

            await DbContext.SaveChangesAsync();

            return true;
        }
        
        
        private async Task<User> GetCurrentUser(HttpContext httpContext)
        {
            return await UserManager.GetCurrentUserAsync(httpContext);
        }
    }
}