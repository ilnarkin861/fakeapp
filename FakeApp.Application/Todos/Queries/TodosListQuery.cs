using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Todos.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка задач юзеров
    /// </summary>
    public class TodosListQuery : IRequest<PaginationResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public bool? Completed { get; set; }
    }
}