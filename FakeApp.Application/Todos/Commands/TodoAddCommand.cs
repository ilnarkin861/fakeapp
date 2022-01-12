using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Todos.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос добавления задачи
    /// </summary>
    public class TodoAddCommand : IRequest<TodoResponse>
    {
        [Required(ErrorMessage = "Todo text is required")]
        public string Text { get; set; }
    }
}