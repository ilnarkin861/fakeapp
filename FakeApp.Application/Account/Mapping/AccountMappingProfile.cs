using AutoMapper;
using FakeApp.Application.Account.Commands;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Account.Mapping
{
    /// <summary>
    /// Правила маппинга при работе с аккаунтом юзера
    /// </summary>
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<User, AccountResponse>();
            
            CreateMap<AccountUpdateCommand, User>();
        }
    }
}