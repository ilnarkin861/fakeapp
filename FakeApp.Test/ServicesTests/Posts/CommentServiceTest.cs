using System;
using System.Threading.Tasks;
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
    public class CommentServiceTest : AppTest
    {
        private const string Identifier = "011854347ead4e588c2a98e18c1eb423";
        private AppDbContext _dbContext;
        private IUserManager _userManager;
        private ICommentService _commentService;


        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _userManager = new UserManager(_dbContext);
            _commentService = new CommentService(_dbContext, GetCommentMapper(), _userManager);
        }


        [Test]
        public async Task CommentsListNotNullTest()
        {
            var comments = await _commentService.GetCommentsListAsync(1, 0, 0);
            
            Assert.NotNull(comments.Data);
            
            Assert.True(comments.Data.Count > 0);
        }


        [Test]
        public async Task CommentsListPaginationObjectNotNullTest()
        {
            var comments = await _commentService.GetCommentsListAsync(1, 0, Settings.CommentsLimit);
            
            Assert.NotNull(comments.Pagination);
        }


        [Test]
        public async Task CommentsLimitEqualsResponseDataCountTest()
        {
            const int limit = 3;

            var comments = await _commentService.GetCommentsListAsync(1, 0, limit);
            
            Assert.True(comments.Data.Count == limit);
        }


        [Test]
        public async Task CommentsLimitNotMoreThanTest()
        {
            var comments = await _commentService.GetCommentsListAsync(1, 0, Settings.CommentsLimit + 10);
            
            Assert.True(comments.Data.Count <= Settings.CommentsLimit);
        }


        [Test]
        public async Task CommentAddTest()
        {
            var postId = new Random().Next(1, 100);

            var comment = new Comment
            {
                PostId = postId,
                Body = "nihil labore qui quis dolor eveniet iste numquam",
                CommentDate = DateTime.Now
            };

            var result = await _commentService.AddCommentAsync(comment, GetCurrentUser(Identifier));

            Assert.NotNull(result);

            Assert.AreEqual(result.Body, comment.Body);
            Assert.True(result.CommentDate == comment.CommentDate);
        }


        [Test]
        public void PostNotFoundWhenAddComment()
        {
            var comment = new Comment
            {
                PostId = 10000,
                Body = "nihil labore qui quis dolor eveniet iste numquam",
                CommentDate = DateTime.Now
            };

            Assert.CatchAsync<RestException>(() => 
                _commentService.AddCommentAsync(comment, GetCurrentUser(Identifier)));
        }
    }
}