using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using FakeApp.Application.Account.Commands;
using FakeApp.Application.Services;
using FakeApp.Application.Shared;
using FakeApp.Domain.Entities;
using FakeApp.Infrastructure.Interfaces;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Application.Account.Handlers
{
    /// <summary>
    /// Handler-класс, вызывающий метод, который обновляет аккаунт юзера
    /// </summary>
    public class AccountUpdateHandler : UserHandler, IRequestHandler<AccountUpdateCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IFileSystemService _fileSystemService;
        
        
        public AccountUpdateHandler(IUserManager userManager, 
            IMapper mapper,
            IHttpService httpService,
            IFileSystemService fileSystemService) : base(userManager, httpService)
        {
            _mapper = mapper;
            _fileSystemService = fileSystemService;
        }
        

        public async Task<User> Handle(AccountUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            if (request.Avatar != null)
            {
                var options = _fileSystemService.GetMediaOptions();
                
                var dir = Path.Combine(options.ImagesDir, options.AvatarsDir);
                
                var imageName = await _fileSystemService.UploadFileAsync(dir, request.Avatar);
                
                user.Avatar = Path.Combine(dir, imageName);
            }
            
            var result = await UserManager.UpdateUserAccountAsync(user, HttpService.GetHttpContext());

            if (!string.IsNullOrEmpty(result.Avatar))
            {
                result.Avatar = $"{HttpService.GetBaseUrl()}/{user.Avatar}";
            }

            return result;
        }
    }
}