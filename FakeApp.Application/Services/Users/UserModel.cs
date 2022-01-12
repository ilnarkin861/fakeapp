namespace FakeApp.Application.Services.Users
{
    /// <summary>
    /// Класс для построения sql запроса, который вытаскивает юзеров из бд
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}