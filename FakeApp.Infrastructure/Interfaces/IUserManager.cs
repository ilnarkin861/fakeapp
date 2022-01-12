using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Infrastructure.Interfaces
{
    /// <summary>
    /// Интерфейс для классов, совершающих операции с юзерами
    /// </summary>
    public interface IUserManager
    {
        Task<PaginationResponse> GetUsersListAsync(int offset, int limit);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetCurrentUserAsync(HttpContext httpContext);
        Task<User> UpdateUserAccountAsync(User user, HttpContext httpContext);
        Task<bool> ChangeUserPasswordAsync(string oldPassword, string newPassword, 
            HttpContext httpContext);
    }
}