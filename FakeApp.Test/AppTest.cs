using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using FakeApp.Application.Albums.Mapping;
using FakeApp.Application.Posts.Mapping;
using FakeApp.Application.Todos.Mapping;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Options;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Test
{
    public abstract class AppTest
    {
        protected AppDbContext GetDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=fakeapptest;Username=ilnar861;Password=qwerty1234")
                .Options;
            
            return new AppDbContext(dbContextOptions);
        }


        protected HttpContext GetCurrentUser(string identifier)
        {
            var httpContext = new DefaultHttpContext();

            var claims = new List<Claim> {new(ClaimTypes.NameIdentifier, identifier)};

            var claimsIdentity = new ClaimsIdentity(claims);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            httpContext.User = claimsPrincipal;

            return httpContext;
        }


        protected IFileSystemService GetFileSystemService()
        {
            var mediaSettings = new MediaOptions
            {
                MediaRootPath = "/home/ilnar861/fakeapp_media_tests",
                ImagesDir = "images",
                AvatarsDir = "avatars",
                PostImagesDir = "posts",
                AlbumCoversDir = "covers",
                PhotosDir = "photos"
            };
            
            var mediaOptions = Options.Create(mediaSettings);

            return new FileSystemService(mediaOptions);
        }


        protected IMapper GetPostMapper()
        {
            return GetMapper(new PostMappingProfile());
        }


        protected IMapper GetCommentMapper()
        {
            return GetMapper(new CommentMappingProfile());
        }


        protected IMapper GetAlbumMapper()
        {
            return GetMapper(new AlbumMappingProfile());
        }


        protected IMapper GetTodoMapper()
        {
            return GetMapper(new TodoMappingProfile());
        }
        
        
        private IMapper GetMapper(Profile profile)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            
            return new Mapper(mapConfig);
        }
    }
}