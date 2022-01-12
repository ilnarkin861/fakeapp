using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FakeApp.Domain.Enum;



namespace FakeApp.Domain.Entities
{
    [Table("fakeapp.user_logins")]
    public class UserLogin
    {
        public int Id { get; set; }
        
        [Required]
        public string Identifier { get; set; }
        public string Password { get; set; }
        public UserLoginType LoginType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}