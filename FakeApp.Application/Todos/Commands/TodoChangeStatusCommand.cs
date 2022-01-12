using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Todos.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос изменения статуса задачи
    /// </summary>
    public class TodoChangeStatusCommand : IRequest<TodoResponse>
    {
        [Required(ErrorMessage = "Todo id is required")]
        public int TodoId { get; set; }
    }
}