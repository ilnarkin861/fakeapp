using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Posts.Services
{
    /// <summary>
    /// Интерфейс для классов, работающих с постами
    /// </summary>
    public interface IPostService
    {
        Task<PaginationResponse> GetPostsListAsync(int offset, int limit, int userId);

        Task<PostResponse> GetPostByIdAsync(int id);
        
        Task<PaginationResponse> GetUserPostsListAsync(int offset, int limit, HttpContext httpContext);
        
        Task<UserPostResponse> GetUserPostAsync(int postId, HttpContext httpContext);
        
        Task<UserPostResponse> AddPostAsync(Post post, HttpContext httpContext);
        
        Task<UserPostResponse> UpdatePostAsync(Post post, HttpContext httpContext);
        
        Task<bool> DeletePostAsync(int id, HttpContext httpContext);
    }
}