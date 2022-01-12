using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FakeApp.Domain.Entities;
using FakeApp.Domain.Enum;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Api.Auth.Services
{
    /// <summary>
    /// Класс для базовой регистрации юзеров
    /// </summary>
    public class BasicSignUpManager : ISignUpManager
    {
        private readonly AppDbContext _dbContext;

        
        public BasicSignUpManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        public async Task<User> SignUpAsync(string identifier, string password, User user)
        {
            var userExists = await _dbContext.Users.AnyAsync(u => u.Email.Equals(user.Email));

            if (userExists)
            {
                throw new RestException("User exists");
            }

            var newUser = new UserLogin
            {
                LoginType = UserLoginType.Basic,
                Identifier = Guid.NewGuid().ToString().Replace("-", ""),
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                User = user
            };
                
            _dbContext.Add(newUser);

            await _dbContext.SaveChangesAsync();

            return newUser.User;
        }
    }
}