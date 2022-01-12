using FakeApp.Domain.Entities;
using FakeApp.Domain.Enum;



namespace FakeApp.Api.Auth.Services.Jwt
{
    /// <summary>
    /// Интерфейс для классов, который генерирует jwt-токен
    /// </summary>
    public interface IJwtGenerator
    {
        string GenerateJwt(string identifier, UserLoginType loginType, User user);
    }
}