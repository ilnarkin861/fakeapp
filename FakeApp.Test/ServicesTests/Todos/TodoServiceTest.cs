using System.Threading.Tasks;
using FakeApp.Application.Services.Users;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FakeApp.Application.Todos;
using FakeApp.Application.Todos.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Exceptions;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Test.ServicesTests.Todos
{
    public class TodoServiceTest : AppTest
    {
        private const string Identifier = "7007b1087df0454c86b8cd47475c7f7f";
        private AppDbContext _dbContext;
        private ITodoService _todoService;
        private IUserManager _userManager;

        
        [SetUp]
        public void SetUp()
        {
            _dbContext = GetDbContext();
            _userManager = new UserManager(_dbContext);
            _todoService = new TodoService(_dbContext, GetTodoMapper(), _userManager);
        }


        [Test]
        public async Task TodosListNotNullOrNotEmptyTest()
        {
            var todos = await _todoService
                .GetTodosListAsync(0, Settings.UserTodosLimit, null, GetCurrentUser(Identifier));
            
            Assert.NotNull(todos.Data);
            
            Assert.True(todos.Data.Count > 0);
        }


        [Test]
        public async Task CompletedTodosListTest()
        {
            var todos = await _todoService
                .GetTodosListAsync(0, Settings.UserTodosLimit, true, GetCurrentUser(Identifier));
            
            Assert.NotNull(todos.Data);
            
            Assert.True(todos.Data.Count > 0);

            foreach (var todo in todos.Data)
            {
                Assert.True(((TodoResponse)todo).Completed);
            }
        }


        [Test]
        public async Task ActiveTodosListTest()
        {
            var todos = await _todoService
                .GetTodosListAsync(0, Settings.UserTodosLimit, false, GetCurrentUser(Identifier));
            
            Assert.NotNull(todos.Data);
            
            Assert.True(todos.Data.Count > 0);

            foreach (var todo in todos.Data)
            {
                Assert.False(((TodoResponse)todo).Completed);
            }
        }


        [Test]
        public async Task TodosListPaginationObjectNotNullTest()
        {
            var todos = await _todoService
                .GetTodosListAsync(0, Settings.UserTodosLimit, null, GetCurrentUser(Identifier));
            
            Assert.NotNull(todos.Pagination);
        }


        [Test]
        public async Task TodosLimitEqualsResponseDataCountTest()
        {
            const int limit = 5;
            
            var todos = await _todoService
                .GetTodosListAsync(0, limit, null, GetCurrentUser(Identifier));
            
            Assert.True(todos.Data.Count == limit);
        }


        [Test]
        public async Task TodosLimitNotMoreThanTest()
        {
            var todos = await _todoService
                .GetTodosListAsync(0, Settings.UserTodosLimit+10, null, GetCurrentUser(Identifier));
            
            Assert.True(todos.Data.Count <= Settings.UserTodosLimit);
        }


        [Test]
        public async Task AddTodoTest()
        {
            var todo = new Todo {Text = "quo adipisci enim quam ut ab"};

            var result = await _todoService.AddTodoAsync(todo, GetCurrentUser(Identifier));

            Assert.NotNull(result);
        }


        [Test]
        public async Task ChangeTodoStatusTest()
        {
            var todoId = await GetLatestTodo();

            var result = await _todoService.SetCompletedTodoStatusAsync(todoId, GetCurrentUser(Identifier));

            Assert.NotNull(result);

            Assert.True(result.Completed);
        }


        [Test]
        public async Task TodoIsCompletedErrorWhenChangeStatusTest()
        {
            var todoId = await GetLatestTodo();

            Assert.CatchAsync<RestException>(() =>
                _todoService.SetCompletedTodoStatusAsync(todoId, GetCurrentUser(Identifier)));
        }


        [Test]
        public void TodoNotFoundWhenChangeStatusTest()
        {
            Assert.CatchAsync<RestException>(() =>
                _todoService.SetCompletedTodoStatusAsync(10000, GetCurrentUser(Identifier)));
        }


        [Test]
        public async Task TodoDeleteTest()
        {
            var todoId = await GetLatestTodo();

            var result = await _todoService.DeleteTodoAsync(todoId, GetCurrentUser(Identifier));
            
            Assert.True(result);

            var todoExists = await _dbContext.Todos.AnyAsync(t => t.Id == todoId);
            
            Assert.False(todoExists);
        }


        [Test]
        public void TodoNotFoundWhenDeleteTest()
        {
            Assert.CatchAsync<RestException>(() => 
                _todoService.DeleteTodoAsync(10000, GetCurrentUser(Identifier)));
        }


        private async Task<int> GetLatestTodo()
        {
            return await _dbContext.Todos.MaxAsync(t => t.Id);
        }
    }
}