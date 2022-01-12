using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Api.Auth.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос авторизации с соц сетями
    /// </summary>
    public class SocialSignInCommand : IRequest<string>
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; }
        
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
    }
}