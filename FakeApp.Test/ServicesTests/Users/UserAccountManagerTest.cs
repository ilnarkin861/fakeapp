using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using FakeApp.Domain.Entities;
using FakeApp.Application.Services.Users;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Test.ServicesTests.Users
{
    public class UserAccountManagerTest : AppTest
    {
        private const string DefaultIdentifier = "0b050318662343a4847bec49a5a7d3f7";
        private IUserManager _userManager;
        

        [SetUp]
        public void SetUp()
        {
            _userManager = new UserManager(GetDbContext());
        }

        
        [Test]
        public async Task CurrentUserAccountInfoTest()
        {
            var user = await _userManager
                .GetCurrentUserAsync(GetCurrentUser("db4c91f642464466b315b381abfc97a8"));

            Assert.NotNull(user);
        }


        [Test]
        public void CurrentUserNotFoundTest()
        {
            Assert.CatchAsync<Exception>(() => _userManager.GetCurrentUserAsync(new DefaultHttpContext()));
        }



        [Test]
        public async Task UpdateUserAccountTest()
        {
            const string identifier = "8a9f0b7ea66842c7b526c86b592a7671";

            var user = new User()
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "67b6e7ea@google.com",
                Phone = "1-463-123-4447",
                WebSite = "conrad.com",
                Avatar = "path/to/avatar.jpg",
                AddressZipCode = "53919-4257",
                AddressCity = "Gwenborough",
                AddressStreet = "Dayna Park",
                AddressSuit = "Apt. 692",
                CompanyName = "Keebler LLC",
                CompanyCatchPhrase = "Multi-layered client-server neural-net",
                CompanyBs = "synergize scalable supply-chains"
            };

            var userAccount = await _userManager.UpdateUserAccountAsync(user, GetCurrentUser(identifier));

            Assert.NotNull(userAccount);

            Assert.AreEqual(user.FirstName, userAccount.FirstName);
            Assert.AreEqual(user.LastName, userAccount.LastName);
            Assert.AreEqual(user.Email, userAccount.Email);
            Assert.AreEqual(user.Phone, userAccount.Phone);
            Assert.AreEqual(user.WebSite, userAccount.WebSite);
            Assert.AreEqual(user.AddressZipCode, userAccount.AddressZipCode);
            Assert.AreEqual(user.AddressCity, userAccount.AddressCity);
            Assert.AreEqual(user.AddressStreet, userAccount.AddressStreet);
            Assert.AreEqual(user.AddressSuit, userAccount.AddressSuit);
            Assert.AreEqual(user.CompanyName, userAccount.CompanyName);
            Assert.AreEqual(user.CompanyCatchPhrase, userAccount.CompanyCatchPhrase);
            Assert.AreEqual(user.CompanyBs, userAccount.CompanyBs);
        }


        [Test]
        public async Task ChangePasswordTest()
        {
            const string identifier = "0b050318662343a4847bec49a5a7d3f7";
            const string oldPassword = "qwerty1234";
            const string newPassword = "qwerty1234";

            var result = await _userManager
                .ChangeUserPasswordAsync(oldPassword, newPassword, GetCurrentUser(identifier));
            
            Assert.True(result);
        }



        [Test]
        public void IncorrectOldPasswordWhenPasswordChangeTest()
        {
            const string oldPassword = "1234567890";
            const string newPassword = "0987654321";

            Assert.CatchAsync<RestException>(() => _userManager
                .ChangeUserPasswordAsync(oldPassword, newPassword, GetCurrentUser(DefaultIdentifier)));
        }
    }
}