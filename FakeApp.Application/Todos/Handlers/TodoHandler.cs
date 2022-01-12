using FakeApp.Application.Services;
using FakeApp.Application.Todos.Services;



namespace FakeApp.Application.Todos.Handlers
{
    /// <summary>
    /// Базовый handler-класс для задач юзеров
    /// </summary>
    public abstract class TodoHandler
    {
        protected readonly ITodoService TodoService;
        protected readonly IHttpService HttpService;

        
        protected TodoHandler(ITodoService todoService, IHttpService httpService)
        {
            TodoService = todoService;
            HttpService = httpService;
        }
    }
}