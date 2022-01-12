using MediatR;



namespace FakeApp.Application.Todos.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос удаления задачи
    /// </summary>
    public class TodoDeleteCommand : IRequest<bool>
    {
        public int TodoId { get; set; }
    }
}