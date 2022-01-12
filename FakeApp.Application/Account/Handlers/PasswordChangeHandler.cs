using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Account.Commands;
using FakeApp.Application.Services;
using FakeApp.Application.Shared;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Account.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который меняет пароль юзера
    /// </summary>
    public class PasswordChangeHandler : UserHandler, IRequestHandler<PasswordChangeCommand, bool>
    {
        public PasswordChangeHandler(IUserManager userManager,
            IHttpService httpService) : base(userManager, httpService)
        {
        }
        
        
        public async Task<bool> Handle(PasswordChangeCommand request, CancellationToken cancellationToken)
        {
            return await UserManager
                .ChangeUserPasswordAsync(request.OldPassword, request.NewPassword, HttpService.GetHttpContext());
        }

        
    }
}