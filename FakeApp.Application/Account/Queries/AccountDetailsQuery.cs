using MediatR;



namespace FakeApp.Application.Account.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос информации авторизованного юзера
    /// </summary>
    public class AccountDetailsQuery : IRequest<AccountResponse>
    {
    }
}