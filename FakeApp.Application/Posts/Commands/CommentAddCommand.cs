using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Posts.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос добавления коммента
    /// </summary>
    public class CommentAddCommand : IRequest<CommentResponse>
    {
        [Required(ErrorMessage = "Post id is required")]
        public int PostId { get; set; }
        
        [Required(ErrorMessage = "Comment body is required")]
        public string Body { get; set; }
    }
}