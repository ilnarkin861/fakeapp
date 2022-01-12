namespace FakeApp.Api.Auth.Services
{
    /// <summary>
    /// Класс для маппинга информации о юзере после регистрации
    /// </summary>
    public class SignUpResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}