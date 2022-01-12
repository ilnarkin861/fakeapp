using MediatR;



namespace FakeApp.Application.Albums.Queries
{
    /// <summary>
    /// Класс для биндинга данных на запрос получения конкретного альбома для текущего
    /// авторизованного юзера
    /// </summary>
    public class UserAlbumDetailsQuery : IRequest<UserAlbumResponse>
    {
        public int Id { get; set; }
    }
}