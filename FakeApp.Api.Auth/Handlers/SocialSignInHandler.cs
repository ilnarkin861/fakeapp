using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Api.Auth.Commands;
using FakeApp.Api.Auth.Services;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Api.Auth.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод авторизации с соц сетями
    /// </summary>
    public class SocialSignInHandler : IRequestHandler<SocialSignInCommand, string>
    {
        private readonly ISignInManager _signInManagerManager;

        public SocialSignInHandler(SocialSignInManager signInManagerManager)
        {
            _signInManagerManager = signInManagerManager;
        }


        public async Task<string> Handle(SocialSignInCommand request, CancellationToken cancellationToken)
        {
            return await _signInManagerManager.SignInAsync(request.Email, null, request.Identifier, request.UserName);
        }
    }
}