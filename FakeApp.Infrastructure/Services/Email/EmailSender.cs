using System.Threading.Tasks;



namespace FakeApp.Infrastructure.Services.Email
{
    /// <summary>
    /// Класс, работающий с почтой
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public Task<bool> SendAsync(string @from, string[] to, string subject, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}