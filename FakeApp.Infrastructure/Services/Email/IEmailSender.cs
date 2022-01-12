using System.Threading.Tasks;



namespace FakeApp.Infrastructure.Services.Email
{
    /// <summary>
    /// Интерфейс для классов, работающих с почтой
    /// </summary>
    public interface IEmailSender
    {
        Task<bool> SendAsync(string from, string[] to, string subject, string message);
    }
}