using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Services;
using FakeApp.Application.Todos.Commands;
using FakeApp.Application.Todos.Services;



namespace FakeApp.Application.Todos.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который удаляет задачу
    /// </summary>
    public class TodoDeleteHandler : TodoHandler, IRequestHandler<TodoDeleteCommand, bool>
    {
        public TodoDeleteHandler(ITodoService todoService, 
            IHttpService httpService) : base(todoService, httpService)
        {
        }
        

        public async Task<bool> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
        {
            return await TodoService.DeleteTodoAsync(request.TodoId, HttpService.GetHttpContext());
        }
    }
}