using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MediatR;
using FakeApp.Infrastructure.Validators;



namespace FakeApp.Application.Posts.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос добавления поста
    /// </summary>
    public class PostAddCommand : IRequest<UserPostResponse>
    {
        [Required(ErrorMessage = "Post title is required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Post body is required")]
        public string Body { get; set; }
        
        [FileSize(1000)]
        [FileType("image")]
        public IFormFile Image { get; set; }
    }
}