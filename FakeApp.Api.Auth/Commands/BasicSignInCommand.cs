using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Api.Auth.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос базовой авторизации
    /// </summary>
    public class BasicSignInCommand : IRequest<string>
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}