using MediatR;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Users.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения конкретного юзера
    /// </summary>
    public class UserDetailsQuery : IRequest<User>
    {
        public int UserId { get; set; }
    }
}