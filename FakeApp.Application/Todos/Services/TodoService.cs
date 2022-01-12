using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FakeApp.Application.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Helpers;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Todos.Services
{
    /// <summary>
    /// Класс для работы с задачами юзеров
    /// </summary>
    public class TodoService : BaseService, ITodoService
    {
        public TodoService(AppDbContext dbContext, IMapper mapper, 
            IUserManager userManager) : base(dbContext, mapper, userManager)
        {
        }
        

        public async Task<PaginationResponse> GetTodosListAsync(int offset, int limit, bool? status, 
            HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);
            
            var todosLimit = limit is 0 or > Settings.UserTodosLimit ? Settings.UserTodosLimit : limit;

            var query = DbContext.Todos.AsNoTracking().Where(t => t.User == user);

            if (status.HasValue)
            {
                query = query.Where(t => t.Completed == status);
            }
            
            var count = await query.CountAsync();

            var todos = await query
                .OrderByDescending(p => p.Id)
                .Skip(offset)
                .Take(todosLimit)
                .ProjectTo<TodoResponse>(Mapper.ConfigurationProvider)
                .ToListAsync();


            return new PaginationResponse
                {
                    Data = todos,
                    Pagination = new Paginator(count, offset, todosLimit)
                };
        }
        

        public async Task<TodoResponse> AddTodoAsync(Todo todo, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            todo.UserId = user.Id;

            DbContext.Add(todo);

            await DbContext.SaveChangesAsync();

            return  Mapper.Map<TodoResponse>(todo);
        }
        

        public async Task<TodoResponse> SetCompletedTodoStatusAsync(int todoId, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var todo = await DbContext.Todos
                .Where(t => t.Id == todoId && t.User == user)
                .FirstOrDefaultAsync();

            if (todo == null)
            {
                throw new RestException("Not found todo for change status");
            }

            if (todo.Completed)
            {
                throw new RestException("Todo status is already completed");
            }

            todo.Completed = true;

            DbContext.Update(todo);

            await DbContext.SaveChangesAsync();

            return Mapper.Map<TodoResponse>(todo);
        }
        

        public async Task<bool> DeleteTodoAsync(int todoId, HttpContext httpContext)
        {
            var user = await GetCurrentUser(httpContext);

            var todo = await DbContext.Todos
                .Where(t => t.Id == todoId && t.User == user)
                .FirstOrDefaultAsync();

            if (todo == null)
            {
                throw new RestException("Not found todo for delete");
            }

            DbContext.Remove(todo);

            await DbContext.SaveChangesAsync();

            return true;
        }


        private async Task<User> GetCurrentUser(HttpContext httpContext)
        {
            return await UserManager.GetCurrentUserAsync(httpContext);
        }
    }
}