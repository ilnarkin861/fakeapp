using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services.Users;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Test.ServicesTests.Albums
{
    public class AlbumServiceTest : AppTest
    {
        private const string Identifier = "7007b1087df0454c86b8cd47475c7f7f";
        private AppDbContext _dbContext;
        private IAlbumService _albumService;
        private IUserManager _userManager;


        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _userManager = new UserManager(_dbContext);
            _albumService = new AlbumService(_dbContext, GetAlbumMapper(), _userManager);
        }


        [Test]
        public async Task AlbumsListNotNullOrNotEmptyTest()
        {
            var albums = await _albumService.GetAlbumsListAsync(0, Settings.AlbumsLimit, 0);
            
            Assert.NotNull(albums.Data);
            
            Assert.True(albums.Data.Count > 0);
        }


        [Test]
        public async Task UserAlbumsListNotNullOrNotEmptyTest()
        {
            var albums = await _albumService.GetAlbumsListAsync(0, Settings.AlbumsLimit, 1);
            
            Assert.NotNull(albums.Data);
            
            Assert.True(albums.Data.Count > 0);
        }


        [Test]
        public async Task AlbumsListPaginationObjectNotNullTest()
        {
            var albums = await _albumService.GetAlbumsListAsync(0, Settings.AlbumsLimit, 0);
            
            Assert.NotNull(albums.Pagination);
        }


        [Test]
        public async Task AlbumsLimitEqualsResponseDataCountTest()
        {
            const int limit = 5;

            var albums = await _albumService.GetAlbumsListAsync(0, limit, 0);
            
            Assert.True(albums.Data.Count == limit);
        }


        [Test]
        public async Task AlbumsLimitNotMoreThanTest()
        {
            var albums = await _albumService.GetAlbumsListAsync(0, Settings.AlbumsLimit + 10, 0);
            
            Assert.True(albums.Data.Count <= Settings.AlbumsLimit);
        }


        [Test]
        public async Task SingleAlbumTest()
        {
            var album = await _albumService.GetAlbumByIdAsync(1);
            
            Assert.NotNull(album);
        }


        [Test]
        public void AlbumNotFoundTest()
        {
            Assert.CatchAsync<EntityNotFoundException>(() => _albumService.GetAlbumByIdAsync(1000000));
        }


        [Test]
        public async Task CurrentUserAlbumsListNotNullOrNotEmptyTest()
        {
            var albums = await _albumService
                .GetUserAlbumsListAsync(1, Settings.UserAlbumsLimit, GetCurrentUser(Identifier));
            
            Assert.NotNull(albums.Data);

            Assert.True(albums.Data.Count > 0);
        }


        [Test]
        public async Task CurrentUserAlbumsListPaginationObjectNotNullTest()
        {
            var albums = await _albumService
                .GetUserAlbumsListAsync(1, Settings.UserAlbumsLimit, GetCurrentUser(Identifier));
            
            Assert.NotNull(albums.Pagination);
        }


        [Test]
        public async Task CurrentUserAlbumsLimitEqualsResponseDataTest()
        {
            const int limit = 3;

            var albums = await _albumService.GetUserAlbumsListAsync(1, limit, GetCurrentUser(Identifier));
            
            Assert.True(albums.Data.Count == limit);
        }


        [Test]
        public async Task CurrentUserAlbumsLimitNotMoreThanTest()
        {
            var albums = await _albumService
                .GetUserAlbumsListAsync(1, Settings.UserAlbumsLimit + 10, GetCurrentUser(Identifier));
            
            Assert.True(albums.Data.Count <= Settings.UserAlbumsLimit);
        }


        [Test]
        public async Task CurrentUserAlbumFoundTest()
        {
            var album = await _albumService.GetUserAlbumAsync(2, GetCurrentUser(Identifier));
            
            Assert.NotNull(album);
        }


        [Test]
        public void CurrentUserAlbumNotFoundTest()
        {
            Assert.CatchAsync<EntityNotFoundException>(() => _albumService
                .GetUserAlbumAsync(10000, GetCurrentUser(Identifier)));
        }


        [Test]
        public async Task AlbumAddTest()
        {
            var album = new Album
            {
                Title = "iste eos nostrum",
                Cover = "path/to/cover.jpg"
            };

            var result = await _albumService.AddAlbumAsync(album, GetCurrentUser(Identifier));

            Assert.NotNull(result);

            Assert.AreEqual(result.Title, album.Title);
            Assert.AreEqual(result.Cover, album.Cover);
        }


        [Test]
        public async Task AlbumUpdateTest()
        {
            var albumId = await GetLatestAlbumId();

            var album = new Album
            {
                Id = albumId,
                Title = "Lorem ipsum dolor sit amet",
                Cover = "path/to/new_cover.jpg"
            };

            var result = await _albumService.UpdateAlbumAsync(album, GetCurrentUser(Identifier));
            
            Assert.NotNull(result);

            Assert.AreEqual(result.Title, album.Title);
            Assert.AreEqual(result.Cover, album.Cover);
        }


        [Test]
        public void AlbumNotFoundWhenUpdateTest()
        {
            var album = new Album
            {
                Id = 10000,
                Title = "Lorem ipsum dolor sit amet",
                Cover = null
            };

            Assert.CatchAsync<RestException>(() => 
                _albumService.UpdateAlbumAsync(album, GetCurrentUser(Identifier)));
        }


        [Test]
        public async Task AlbumDeleteTest()
        {
            var albumId = await GetLatestAlbumId();

            var result = await _albumService.DeleteAlbumAsync(albumId, GetCurrentUser(Identifier));

            var albumExists = await _dbContext.Albums.AnyAsync(a => a.Id == albumId);
            
            Assert.True(result);
            
            Assert.False(albumExists);
        }


        [Test]
        public void AlbumNotFoundWhenDeleteTest()
        {
            Assert.CatchAsync<RestException>(() => _albumService
                .DeleteAlbumAsync(10000, GetCurrentUser(Identifier)));
        }


        private async Task<int> GetLatestAlbumId()
        {
            return await _dbContext.Albums.MaxAsync(a => a.Id);
        }
    }
}