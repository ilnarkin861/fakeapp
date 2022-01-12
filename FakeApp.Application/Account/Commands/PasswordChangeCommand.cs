using System.ComponentModel.DataAnnotations;
using MediatR;



namespace FakeApp.Application.Account.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос смены пароля юзера
    /// </summary>
    public class PasswordChangeCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Old password is required")]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "New password is required")]
        [MinLength(8, ErrorMessage = "Password length must not be less than 8 characters")]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Please, confirm your password")]
        [Compare("NewPassword", ErrorMessage = "Passwords does not match")]
        public string NewPasswordConfirm { get; set; }
    }
}