using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MediatR;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Validators;



namespace FakeApp.Application.Account.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос обновления аккаунта юзера
    /// </summary>
    public class AccountUpdateCommand : IRequest<User>
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string WebSite { get; set; }
        
        [FileSize(500)]
        [FileType("image")]
        public IFormFile Avatar { get; set; }
        
        public string AddressZipCode { get; set; }
        
        public string AddressCity { get; set; }
        
        public string AddressStreet { get; set; }
        
        public string AddressSuit { get; set; }
        
        public string CompanyName { get; set; }
        
        public string CompanyCatchPhrase { get; set; }
        
        public string CompanyBs { get; set; }
    }
}