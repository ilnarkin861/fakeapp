namespace FakeApp.Application.Albums
{
    /// <summary>
    /// Класс для маппинга данных об альбоме текущего авторизованного юзера
    /// </summary>
    public class UserAlbumResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
    }
}