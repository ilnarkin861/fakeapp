using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Todos.Services
{
    /// <summary>
    /// Интерфейс для классов, работающих с задачами юзеров
    /// </summary>
    public interface ITodoService
    {
        Task<PaginationResponse> GetTodosListAsync(int offset, int limit, bool? status, HttpContext httpContext);
        Task<TodoResponse> AddTodoAsync(Todo todo, HttpContext httpContext);
        Task<TodoResponse> SetCompletedTodoStatusAsync(int todoId, HttpContext httpContext);
        Task<bool> DeleteTodoAsync(int todoId, HttpContext httpContext);
    }
}