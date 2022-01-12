using System.Threading.Tasks;



namespace FakeApp.Infrastructure.Interfaces
{
    /// <summary>
    /// Интерфейс для классов, авторизирующих юзеров
    /// </summary>
    public interface ISignInManager
    {
        Task<string> SignInAsync(string email, string password, string identifier, string userName);
    }
}