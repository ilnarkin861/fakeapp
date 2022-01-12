using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Services;
using FakeApp.Application.Todos.Commands;
using FakeApp.Application.Todos.Services;



namespace FakeApp.Application.Todos.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который изменяет статус задачи
    /// </summary>
    public class TodoChangeStatusHandler : TodoHandler, IRequestHandler<TodoChangeStatusCommand, TodoResponse>
    {
        public TodoChangeStatusHandler(ITodoService todoService, 
            IHttpService httpService) : base(todoService, httpService)
        {
        }
        

        public async Task<TodoResponse> Handle(TodoChangeStatusCommand request, 
            CancellationToken cancellationToken)
        {
            return await TodoService.SetCompletedTodoStatusAsync(request.TodoId, HttpService.GetHttpContext());
        }
    }
}