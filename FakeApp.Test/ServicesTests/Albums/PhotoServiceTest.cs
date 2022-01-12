using System.Collections.Generic;
using System.Linq;
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
    public class PhotoServiceTest : AppTest
    {
        private const string Identifier = "7007b1087df0454c86b8cd47475c7f7f";
        private AppDbContext _dbContext;
        private IUserManager _userManager;
        private PhotosService _photosService;


        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _userManager = new UserManager(_dbContext);
            _photosService = new PhotosService(_dbContext, GetAlbumMapper(), _userManager);
        }


        [Test]
        public async Task PhotosListNotNullOrNotEmptyTest()
        {
            var photos = await _photosService.GetPhotosAsync(1, 0, Settings.PhotosLimit);
            
            Assert.NotNull(photos.Data);
            
            Assert.True(photos.Data.Count > 0);
        }


        [Test]
        public async Task PhotosListPaginationObjectNotNullTest()
        {
            var photos = await _photosService.GetPhotosAsync(1, 0, Settings.PhotosLimit);
            
            Assert.NotNull(photos.Pagination);
        }


        [Test]
        public async Task PhotosLimitEqualsResponseDataCountTest()
        {
            const int limit = 5;

            var photos = await _photosService.GetPhotosAsync(1, 0, limit);
            
            Assert.True(photos.Data.Count == limit);
        }


        [Test]
        public async Task PhotosLimitNotMoreThanTest()
        {
            var photos = await _photosService.GetPhotosAsync(1, 0, Settings.PhotosLimit+10);
            
            Assert.True(photos.Data.Count <= Settings.PhotosLimit);
        }


        [Test]
        public async Task CurrentUserPhotosListNotNullOrNotEmptyTest()
        {
            var photos = await _photosService
                .GetPhotosAsync(2, 1, Settings.UserPhotosLimit, GetCurrentUser(Identifier));
            
            Assert.NotNull(photos.Data);
            
            Assert.True(photos.Data.Count > 0);
        }


        [Test]
        public async Task CurrentUserPhotosListPaginationObjectNotNullTest()
        {
            var photos = await _photosService
                .GetPhotosAsync(2, 1, Settings.UserPhotosLimit, GetCurrentUser(Identifier));
            
            Assert.NotNull(photos.Pagination);
        }


        [Test]
        public async Task CurrentUserPhotosLimitEqualsResponseDataCountTest()
        {
            const int limit = 5;
            
            var photos = await _photosService.GetPhotosAsync(2, 0, limit, GetCurrentUser(Identifier));
            
            Assert.True(photos.Data.Count == limit);
        }


        [Test]
        public async Task CurrentUserPhotosLimitNotMoreThanTest()
        {
            var photos = await _photosService
                .GetPhotosAsync(1, 0, Settings.UserPhotosLimit+10, GetCurrentUser(Identifier));
            
            Assert.True(photos.Data.Count <= Settings.PhotosLimit);
        }


        [Test]
        public async Task UploadPhotosTest()
        {
            var photosList = new List<Photo>();

            for (var i = 0; i < 5; i++)
            {
                photosList.Add(new Photo{Url = $"path/to/photo_{i.ToString()}.jpg"});
            }

            var photos = await _photosService.UploadPhotosAsync(2, photosList, GetCurrentUser(Identifier));

            Assert.NotNull(photos);
        }


        [Test]
        public void AlbumNotFoundWhenUploadPhotosTest()
        {
            Assert.CatchAsync<RestException>(() =>
                _photosService.UploadPhotosAsync(10000, new List<Photo>(), GetCurrentUser(Identifier)));
        }


        [Test]
        public async Task PhotoDeleteTest()
        {
            var photoId = await _dbContext.Photos.Where(p => p.Album.UserId == 1).MaxAsync(p => p.Id);

            var result = await _photosService.DeletePhotoAsync(2, photoId, GetCurrentUser(Identifier));
            
            Assert.True(result);

            var photoExists = await _dbContext.Photos.AnyAsync(p => p.Id == photoId);
            
            Assert.False(photoExists);
        }


        [Test]
        public void PhotoNotFoundWhenDeletingTest()
        {
            Assert.CatchAsync<RestException>(() =>
                _photosService.DeletePhotoAsync(100000, 1, GetCurrentUser(Identifier)));
        }
    }
}