using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FakeApp.Api.Auth.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Test.Auth.ServicesTests
{
    public class SignUpServiceTest : AppTest
    {
        private AppDbContext _dbContext;
        private ISignUpManager _signUpManager;


        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _signUpManager = new BasicSignUpManager(_dbContext);
        }


        [Test]
        public async Task BasicSignUpSuccessTest()
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Smith",
                Email = $"{Guid.NewGuid().ToString().Split("-")[0]}@smith.com"
            };

            const string password = "qwerty1234";

            var signUpResult = await _signUpManager.SignUpAsync(null, password, user);
            
            Assert.NotNull(signUpResult);
            
            Assert.AreEqual(user.FirstName, signUpResult.FirstName);
            Assert.AreEqual(user.LastName, signUpResult.LastName);
            Assert.AreEqual(user.Email, signUpResult.Email);
        }


        [Test]
        public async Task UserExistsWhenBasicSignUpTest()
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "Sincere@april.biz"
            };

            var userExists = await _dbContext.Users.AnyAsync(u => u.Email == user.Email);
            
            Assert.True(userExists);

            Assert.CatchAsync<RestException>(() => _signUpManager.SignUpAsync(null, "1234", user));

        }
    }
}