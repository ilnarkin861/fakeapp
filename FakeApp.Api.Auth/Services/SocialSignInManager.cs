using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FakeApp.Api.Auth.Services.Jwt;
using FakeApp.Domain.Entities;
using FakeApp.Domain.Enum;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Api.Auth.Services
{
    /// <summary>
    /// Класс для авторизации юзеров соц сетями
    /// </summary>
    public class SocialSignInManager : ISignInManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IJwtGenerator _jwtGenerator;


        public SocialSignInManager(AppDbContext dbContext, IJwtGenerator jwtGenerator)
        {
            _dbContext = dbContext;
            _jwtGenerator = jwtGenerator;
        }


        public async Task<string> SignInAsync(string email, string password, string identifier, string userName)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new RestException("Wrong identifier");
            }

            var user = await _dbContext.UserLogins
                .AsNoTracking()
                .Include(l => l.User)
                .Where(u => u.Identifier == identifier 
                            && u.User.Email == email
                            && u.LoginType == UserLoginType.Social)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return _jwtGenerator.GenerateJwt(user.Identifier, user.LoginType, user.User);
            }
            
            var userExists = await _dbContext.Users.AnyAsync(u => u.Email == email);
                
            if (userExists)
            {
                throw new RestException("User with this email address registered earlier");
            }
            
            var newUser = new UserLogin
            {
                LoginType = UserLoginType.Social,
                Identifier = identifier,
                User = new User {Email = email, FirstName = userName}
            };
            
            _dbContext.Add(newUser);

            await _dbContext.SaveChangesAsync();
      
            return _jwtGenerator.GenerateJwt(identifier, newUser.LoginType, newUser.User);
        }
    }
}