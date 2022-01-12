using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FakeApp.Application.Services;
using FakeApp.Application.Todos.Commands;
using FakeApp.Application.Todos.Services;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Todos.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который добавляет задачу
    /// </summary>
    public class TodoAddHandler : TodoHandler, IRequestHandler<TodoAddCommand, TodoResponse>
    {
        private readonly IMapper _mapper;
        
        
        public TodoAddHandler(ITodoService todoService, IHttpService httpService, 
            IMapper mapper) : base(todoService, httpService)
        {
            _mapper = mapper;
        }

        
        public async Task<TodoResponse> Handle(TodoAddCommand request, CancellationToken cancellationToken)
        {
            return await TodoService.AddTodoAsync(_mapper.Map<Todo>(request), HttpService.GetHttpContext());
        }
    }
}