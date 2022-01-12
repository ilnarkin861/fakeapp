using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Services;
using FakeApp.Application.Shared;
using FakeApp.Application.Users.Queries;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Users.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который получает конкретного юзера
    /// </summary>
    public class UserDetailsHandler : UserHandler, IRequestHandler<UserDetailsQuery, User>
    {
        public UserDetailsHandler(IUserManager userManager, 
            IHttpService httpService) : base(userManager, httpService)
        {
        }

        public async Task<User> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserByIdAsync(request.UserId);

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar = $"{HttpService.GetBaseUrl()}/{user.Avatar}";
            }

            return user;
        }
    }
}