namespace FakeApp.Application.Albums
{
    /// <summary>
    /// Класс для маппинга данных о фотках в альбоме текущего авторизованного юзера
    /// </summary>
    public class UserPhotosResponse
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}