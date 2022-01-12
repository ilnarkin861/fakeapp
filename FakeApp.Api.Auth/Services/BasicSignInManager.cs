using System.Linq;
using System.Threading.Tasks;
using FakeApp.Api.Auth.Services.Jwt;
using FakeApp.Domain.Enum;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeApp.Api.Auth.Services
{
    /// <summary>
    /// Класс для базовой авторизации юзеров
    /// </summary>
    public class BasicSignInManager : ISignInManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IJwtGenerator _jwtGenerator;

        
        public BasicSignInManager(AppDbContext dbContext, IJwtGenerator jwtGenerator)
        {
            _dbContext = dbContext;
            _jwtGenerator = jwtGenerator;
        }


        public async Task<string> SignInAsync(string email, string password, string identifier, string userName)
        {
            var user = await _dbContext.UserLogins
                .AsNoTracking()
                .Include(l => l.User)
                .Where(u => u.User.Email.Equals(email) && u.LoginType == UserLoginType.Basic)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new RestException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new RestException("Incorrect password");
            }

            return _jwtGenerator.GenerateJwt(user.Identifier, user.LoginType, user.User);
        }
    }
}