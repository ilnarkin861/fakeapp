using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FakeApp.Domain.Entities
{
    /// <summary>
    /// Класс модели в бд для юзеров
    /// </summary>
    [Table("fakeapp.users")]
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string Avatar { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressSuit { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCatchPhrase { get; set; }
        public string CompanyBs { get; set; }
    }
}