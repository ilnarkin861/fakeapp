using FakeApp.Application.Services;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Shared
{
    /// <summary>
    /// Базовый handler-класс для юзеров
    /// </summary>
    public abstract class UserHandler
    {
        protected readonly IUserManager UserManager;
        protected readonly IHttpService HttpService;

        
        protected UserHandler(IUserManager userManager, IHttpService httpService)
        {
            UserManager = userManager;
            HttpService = httpService;
        }
    }
}