using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Services;
using FakeApp.Application.Todos.Queries;
using FakeApp.Application.Todos.Services;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Todos.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает список задач
    /// </summary>
    public class TodosListHandler : TodoHandler, IRequestHandler<TodosListQuery, PaginationResponse>
    {
        public TodosListHandler(ITodoService todoService,
            IHttpService httpService) : base(todoService, httpService)
        {
        }
        

        public async Task<PaginationResponse> Handle(TodosListQuery request,
            CancellationToken cancellationToken)
        {
            return await TodoService
                .GetTodosListAsync(request.Offset, request.Limit, request.Completed, HttpService.GetHttpContext());
        }
    }
}