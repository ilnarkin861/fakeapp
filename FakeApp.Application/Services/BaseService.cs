using AutoMapper;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Interfaces;



namespace FakeApp.Application.Services
{
    /// <summary>
    /// Базовый класс для сервис-классов
    /// </summary>
    public abstract class BaseService
    {
        protected readonly AppDbContext DbContext;
        protected readonly IMapper Mapper;
        protected readonly IUserManager UserManager;

        
        protected BaseService(AppDbContext dbContext, IMapper mapper, IUserManager userManager)
        {
            DbContext = dbContext;
            Mapper = mapper;
            UserManager = userManager;
        }
    }
}