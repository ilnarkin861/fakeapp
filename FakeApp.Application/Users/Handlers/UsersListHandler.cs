using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FakeApp.Application.Services;
using FakeApp.Application.Services.Users;
using FakeApp.Application.Shared;
using FakeApp.Application.Users.Queries;
using FakeApp.Infrastructure.DTO;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Users.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который список юзеров
    /// </summary>
    public class UsersListHandler : UserHandler, IRequestHandler<UsersListQuery, PaginationResponse>
    {
        public UsersListHandler(IUserManager userManager, 
            IHttpService httpService) : base(userManager, httpService)
        {
        }
        

        public async Task<PaginationResponse> Handle(UsersListQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await UserManager.GetUsersListAsync(request.Offset, request.Limit);

            foreach (var data in result.Data)
            {
                var user = (UserModel) data;

                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    user.Avatar = $"{HttpService.GetBaseUrl()}/{user.Avatar}";
                }
            }

            return result;
        }
    }
}