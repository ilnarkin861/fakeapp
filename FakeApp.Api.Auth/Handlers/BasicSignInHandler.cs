using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Api.Auth.Commands;
using FakeApp.Api.Auth.Services;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Api.Auth.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод базовой авторизации
    /// </summary>
    public class BasicSignInHandler : IRequestHandler<BasicSignInCommand, string>
    {
        private readonly ISignInManager _signInManagerManager;

        
        public BasicSignInHandler(BasicSignInManager signInManagerManager)
        {
            _signInManagerManager = signInManagerManager;
        }


        public async Task<string> Handle(BasicSignInCommand request, CancellationToken cancellationToken)
        {
            return await _signInManagerManager.SignInAsync(request.Email, request.Password, null, null);
        }
    }
}