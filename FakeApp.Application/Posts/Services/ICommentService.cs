using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Posts.Services
{
    /// <summary>
    /// Интерфейс для классов, работающих с комментами
    /// </summary>
    public interface ICommentService
    {
        Task<PaginationResponse> GetCommentsListAsync(int postId, int offset, int limit);
        Task<CommentResponse> AddCommentAsync(Comment comment, HttpContext httpContext);
    }
}