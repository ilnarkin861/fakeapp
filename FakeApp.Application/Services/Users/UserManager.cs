using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FakeApp.Domain.Entities;
using FakeApp.Domain.Enum;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Helpers;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Services.Users
{
    /// <summary>
    /// Класс для работы с юзерами
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly AppDbContext _dbContext;

        
        public UserManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<PaginationResponse> GetUsersListAsync(int offset, int limit)
        {
            var usersLimit = limit is 0 or > Settings.UsersLimit ? Settings.UsersLimit : limit;

            var query = _dbContext.Users.AsNoTracking();
            
            var count = await query.CountAsync();

            var users = await query
                .OrderByDescending(u => u.Id)
                .Skip(offset)
                .Take(usersLimit)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Avatar = u.Avatar,
                    Phone = u.Phone,
                    Website = u.WebSite
                }).ToListAsync();

            return new PaginationResponse
            {
                Data = users,
                Pagination = new Paginator(count, offset, usersLimit)
            };
        }
        

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }
            return user;
        }
        

        public async Task<User> GetCurrentUserAsync(HttpContext httpContext)
        {
            var identifier = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var result = await _dbContext.UserLogins
                .AsNoTracking()
                .Include(l => l.User)
                .Where(l => l.Identifier.Equals(identifier))
                .FirstOrDefaultAsync();

            return result?.User;
        }
        
        
        public async Task<User> UpdateUserAccountAsync(User user, HttpContext httpContext)
        {
            var currentUser = await GetCurrentUserAsync(httpContext);

            user.Id = currentUser.Id;

            if (string.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar = currentUser.Avatar;
            }

            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }
        

        public async Task<bool> ChangeUserPasswordAsync(string oldPassword, string newPassword, 
            HttpContext httpContext)
        {
            var identifier = httpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;

            var result = await _dbContext.UserLogins
                .Where(l => l.Identifier.Equals(identifier) 
                            && l.LoginType == UserLoginType.Basic)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new RestException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, result.Password))
            {
                throw new RestException("Old password incorrect");
            }

            result.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _dbContext.Update(result);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}