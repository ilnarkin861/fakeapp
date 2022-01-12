using System.Threading.Tasks;
using FakeApp.Domain.Entities;



namespace FakeApp.Infrastructure.Interfaces
{
    /// <summary>
    /// Интерфейс для классов, регистрирующих юзеров
    /// </summary>
    public interface ISignUpManager
    {
        Task<User> SignUpAsync(string identifier, string password, User user);
    }
}