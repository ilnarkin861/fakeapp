using MediatR;
using FakeApp.Infrastructure.DTO;



namespace FakeApp.Application.Users.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения списка юзеров
    /// </summary>
    public class UsersListQuery : IRequest<PaginationResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}