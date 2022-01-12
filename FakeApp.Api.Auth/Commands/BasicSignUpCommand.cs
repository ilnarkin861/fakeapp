using System.ComponentModel.DataAnnotations;
using MediatR;
using FakeApp.Api.Auth.Services;



namespace FakeApp.Api.Auth.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос базовой регистрации
    /// </summary>
    public class BasicSignUpCommand : IRequest<SignUpResponse>
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password length must not be less than 8 characters")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please, confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string PasswordConfirm { get; set; }
    }
}