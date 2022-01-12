using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using FakeApp.Api.Auth.Services;
using FakeApp.Api.Auth.Services.Jwt;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;
using FakeApp.Infrastructure.Options;



namespace FakeApp.Test.Auth.ServicesTests
{
    public class SignInServiceTest : AppTest
    {
        private AppDbContext _dbContext;
        private ISignInManager _basicSignInManager;
        private ISignInManager _socialSignInManager;


        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _basicSignInManager = new BasicSignInManager(_dbContext, GetJwtGenerator());
            _socialSignInManager = new SocialSignInManager(_dbContext, GetJwtGenerator());
        }


        [Test]
        public async Task SignInSuccessTest()
        {
            const string identifier = "108274830447282198902";
            const string userName = "John";
            const string email = "Rey.Padberg@karina.biz";
            const string socialEmail = "testuser@gmail.com";
            const string password = "qwerty1234";

            var basicSignInResult = await _basicSignInManager.SignInAsync(email, password, null, null);
            var socialSignInResult = await _socialSignInManager.SignInAsync(socialEmail, null, identifier, userName);
            
            Assert.NotNull(basicSignInResult);
            Assert.NotNull(socialSignInResult);
            
            Assert.IsInstanceOf<string>(basicSignInResult);
            Assert.IsInstanceOf<string>(socialSignInResult);
        }


        [Test]
        public async Task UserNotFoundWhenBasicSignInTest()
        {
            const string wrongEmail = "asdfg123@example.com";

            var userExists = await _dbContext.Users.AnyAsync(u => u.Email == wrongEmail);
            
            Assert.False(userExists);

            Assert.CatchAsync<RestException>(async () => await _basicSignInManager
                .SignInAsync(wrongEmail, "1234", null, null));
        }


        [Test]
        public void IncorrectPasswordWhenBasicSignInTest()
        {
            const string email = "Sincere@april.biz";
            const string incorrectPassword = "asdfge1234";
            
            Assert.CatchAsync<RestException>(() => _basicSignInManager
                .SignInAsync(email, incorrectPassword, null, null));
        }


        [Test]
        public async Task SocialSignInWhenUserNotFoundTest()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var email = $"{identifier[..5]}@google.com";

            var userNotFound = await _dbContext.Users.AnyAsync(u => u.Email == email);
            
            Assert.False(userNotFound);

            var signInResult = await _socialSignInManager.SignInAsync(email, null, identifier, "John");
            
            Assert.NotNull(signInResult);
            
            Assert.IsInstanceOf<string>(signInResult);
            
            var userNExist = await _dbContext.Users.AnyAsync(u => u.Email == email);
            
            Assert.True(userNExist);
        }
        
        
        public IJwtGenerator GetJwtGenerator()
        {
            var authSettings = new AuthOptions
            {
                Issuer = "FakeApp.Auth",
                Audience = "FakeApp.Api",
                Secret = "y6nwcqdg9st3mywke4odhk72v7vwotic",
                TokenLifetime = 1209600

            };

            var authOptions = Options.Create(authSettings);
            
            return new JwtGenerator(authOptions);
        }
    }
}