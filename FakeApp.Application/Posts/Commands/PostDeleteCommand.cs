using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Posts.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос удаления поста
    /// </summary>
    public class PostDeleteCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Post id is required")]
        public int PostId { get; set; }
    }
}