using System.Text;
using Microsoft.IdentityModel.Tokens;



namespace FakeApp.Infrastructure.Options
{
    /// <summary>
    /// Класс для авторизационных данных
    /// </summary>
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}