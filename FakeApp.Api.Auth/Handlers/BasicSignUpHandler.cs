using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Api.Auth.Commands;
using FakeApp.Api.Auth.Services;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Api.Auth.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод базовой регистрации
    /// </summary>
    public class BasicSignUpHandler : IRequestHandler<BasicSignUpCommand, SignUpResponse>
    {
        private readonly ISignUpManager _signUpManager;

        
        public BasicSignUpHandler(ISignUpManager signUpManager)
        {
            _signUpManager = signUpManager;
        }

        public async Task<SignUpResponse> Handle(BasicSignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email

            };

            var result = await _signUpManager.SignUpAsync(null, request.Password, user);
            
            return new SignUpResponse
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
            };
        }
    }
}