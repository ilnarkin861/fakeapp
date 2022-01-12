using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FakeApp.Application.Account.Queries;
using FakeApp.Application.Services;
using FakeApp.Application.Shared;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Account.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который выдает инфу об аккаунте юзера
    /// </summary>
    public class AccountDetailsHandler : UserHandler, IRequestHandler<AccountDetailsQuery, AccountResponse>
    {
        private readonly IMapper _mapper;


        public AccountDetailsHandler(IUserManager userManager,
            IHttpService httpService,
            IMapper mapper) : base(userManager, httpService)
        {
            _mapper = mapper;
        }


        public async Task<AccountResponse> Handle(AccountDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetCurrentUserAsync(HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar = $"{HttpService.GetBaseUrl()}/{user.Avatar}";
            }

            return _mapper.Map<AccountResponse>(user);
        }
    }
}