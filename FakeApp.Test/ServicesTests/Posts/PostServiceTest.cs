using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services.Users;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Test.ServicesTests.Posts
{
    public class PostServiceTest : AppTest
    {
        private const string Identifier = "7007b1087df0454c86b8cd47475c7f7f";
        private AppDbContext _dbContext;
        private IPostService _postService;
        private IUserManager _userManager;


        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _userManager = new UserManager(_dbContext);
            _postService = new PostService(_dbContext, GetPostMapper(), _userManager);
        }


        [Test]
        public async Task PostsListNotNullOrNotEmptyTest()
        {
            var posts = await _postService.GetPostsListAsync(0, Settings.PostsLimit, 0);
            
            Assert.NotNull(posts.Data);
            
            Assert.True(posts.Data.Count > 0);
        }
        
        
        [Test]
        public async Task UserPostsListNotNullOrNotEmptyTest()
        {
            var posts = await _postService.GetPostsListAsync(0, Settings.PostsLimit, 2);
            
            Assert.NotNull(posts.Data);
            
            Assert.True(posts.Data.Count > 0);
        }


        [Test]
        public async Task PostsListPaginationObjectNotNullTest()
        {
            var posts = await _postService.GetPostsListAsync(0, Settings.PostsLimit, 0);
            
            Assert.NotNull(posts.Pagination);
        }


        [Test]
        public async Task PostsLimitEqualsResponseDataCountTest()
        {
            const int limit = 3;

            var posts = await _postService.GetPostsListAsync(0, limit, 0);
            
            Console.WriteLine(posts.Data.Count);

            Assert.True(posts.Data.Count == limit);
        }


        [Test]
        public async Task PostsLimitNotMoreThanTest()
        {
            var posts = await _postService.GetPostsListAsync(0, Settings.PostsLimit + 100, 0);
            
            Assert.True(posts.Data.Count <= Settings.PostsLimit);
        }


        [Test]
        public async Task PostFoundTest()
        {
            var post = await _postService.GetPostByIdAsync(1);
            
            Assert.NotNull(post);
        }


        [Test]
        public void PostNotFoundTest()
        {
            Assert.CatchAsync<EntityNotFoundException>(() => _postService.GetPostByIdAsync(100000));
        }

        
        [Test]
        public async Task CurrentUserPostsListNotNullOrNotEmptyTest()
        {
            var posts = await _postService
                .GetUserPostsListAsync(0, Settings.PostsLimit, GetCurrentUser(Identifier));
            
            Assert.NotNull(posts.Data);
            
            Assert.True(posts.Data.Count > 0);
        }
        

        [Test]
        public async Task CurrentUserPostsListPaginationObjectNotNullTest()
        {
            var posts = await _postService.GetUserPostsListAsync(0, 0, GetCurrentUser(Identifier));
            
            Assert.NotNull(posts.Pagination);
        }


        [Test]
        public async Task CurrentUserPostsLimitEqualsResponseDataTest()
        {
            const int limit = 5;

            var posts = await _postService.GetUserPostsListAsync(0, limit, GetCurrentUser(Identifier));
            
            Assert.True(posts.Data.Count == limit);
        }


        [Test]
        public async Task CurrentUserPostsLimitNotMoreThanTest()
        {
            var posts = await _postService
                .GetUserPostsListAsync(0, Settings.UserPostsLimit + 10, GetCurrentUser(Identifier));
            
            Assert.True(posts.Data.Count <= Settings.UserPostsLimit);
        }


        [Test]
        public async Task CurrentUserPostFoundTest()
        {
            var post = await _postService.GetUserPostAsync(12, GetCurrentUser(Identifier));
            
            Assert.NotNull(post);
        }
        
        
        [Test]
        public void CurrentUserPostNotFoundTest()
        {
            Assert.CatchAsync<EntityNotFoundException>(() =>
                _postService.GetUserPostAsync(100000, GetCurrentUser(Identifier)));
        }


        [Test]
        public async Task PostAddTest()
        {
            var post = new Post
            {
                Title = "qui est esse",
                Body = @"ullam et saepe reiciendis voluptatem adipisci
                    sit amet autem assumenda provident rerum culpa
                    quis hic commodi nesciunt rem tenetur doloremque ipsam iure
                    quis sunt voluptatem rerum illo velit",
                Image = "path/to/image.jpg"
            };

            var result = await _postService.AddPostAsync(post, GetCurrentUser(Identifier));

            Assert.NotNull(result);

            Assert.AreEqual(result.Title, post.Title);
            Assert.AreEqual(result.Body, post.Body);
            Assert.AreEqual(result.Image, post.Image);
            Assert.True(result.CreatedDate == post.CreatedDate);
        }


        [Test]
        public async Task PostUpdateTest()
        {
            var postId = await GetLatestPostId();

            var post = new Post
            {
                Id = postId,
                Title = "aut amet sed",
                Body = "libero voluptate eveniet aperiam sed sunt placeat suscipit molestias",
                Image = "path/to/new_image.jpg"
            };
            
            var result = await _postService.UpdatePostAsync(post, GetCurrentUser(Identifier));
            
            Assert.AreEqual(result.Title, post.Title);
            Assert.AreEqual(result.Body, post.Body);
            Assert.AreEqual(result.Image, post.Image);
            Assert.True(result.CreatedDate == post.CreatedDate);
        }


        [Test]
        public void PostNotFoundWhenUpdate()
        {
            var post = new Post
            {
                Id = 10000,
                Title = "aut amet sed",
                Body = "libero voluptate eveniet aperiam sed sunt placeat suscipit molestias",
                CreatedDate = DateTime.Now,
                Image = null
            };

            Assert.CatchAsync<RestException>(() => 
                _postService.UpdatePostAsync(post, GetCurrentUser(Identifier)));
        }
        


        [Test]
        public async Task PostDeleteTest()
        {
            var postId = await GetLatestPostId();

            var result = await _postService.DeletePostAsync(postId, GetCurrentUser(Identifier));

            var postExists = await _dbContext.Posts.AnyAsync(p => p.Id == postId);
            
            Assert.True(result);
            
            Assert.False(postExists);
        }
        
        
        [Test]
        public void PostNotFoundWhenDelete()
        {
            Assert.CatchAsync<RestException>(() =>
                _postService.DeletePostAsync(10000, GetCurrentUser(Identifier)));
        }


        private async Task<int> GetLatestPostId()
        {
            return await _dbContext.Posts.MaxAsync(p => p.Id);
        }
    }
}