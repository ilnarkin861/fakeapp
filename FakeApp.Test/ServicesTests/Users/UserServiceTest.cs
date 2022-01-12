using System.Threading.Tasks;
using NUnit.Framework;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;
using FakeApp.Application.Services.Users;



namespace FakeApp.Test.ServicesTests.Users
{
    public class UserServiceTest : AppTest
    {
        private IUserManager _userManager;


        [SetUp]
        public void Setup()
        {
            _userManager = new UserManager(GetDbContext());
        }


        [Test]
        public async Task UsersListNotNullOrNotEmptyTest()
        {
            var users = await _userManager.GetUsersListAsync(0, Settings.UsersLimit);
            
            Assert.NotNull(users.Data);
            
            Assert.True(users.Data.Count > 0);
        }


        [Test]
        public async Task PaginationObjectNotNullTest()
        {
            var users = await _userManager.GetUsersListAsync(0, Settings.UsersLimit);
            
            Assert.NotNull(users.Pagination);
        }


        [Test]
        public async Task UsersLimitEqualsResponseDataCountTest()
        {
            const int limit = 3;

            var users = await _userManager.GetUsersListAsync(0, limit);
            
            Assert.True(users.Data.Count == limit);
        }


        [Test]
        public async Task UsersLimitNotMoreThanTest()
        {
            var users = await _userManager.GetUsersListAsync(0, Settings.UsersLimit + 10);
            
            Assert.True(users.Data.Count <= Settings.UsersLimit);
        }


        [Test]
        public async Task UserFoundTest()
        {
            var user = await _userManager.GetUserByIdAsync(1);
            
            Assert.NotNull(user);
        }
        
        
        [Test]
        public void UserNotFoundTest()
        {
            Assert.CatchAsync<EntityNotFoundException>(() => _userManager.GetUserByIdAsync(1000000));
        }
    }
}