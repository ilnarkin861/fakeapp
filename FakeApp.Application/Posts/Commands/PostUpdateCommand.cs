using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MediatR;
using FakeApp.Infrastructure.Validators;



namespace FakeApp.Application.Posts.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос обновления поста
    /// </summary>
    public class PostUpdateCommand : IRequest<UserPostResponse>
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Post title is required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Post body is required")]
        public string Body { get; set; }
        
        [FileSize(1000)]
        [FileType("image")]
        public IFormFile Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}