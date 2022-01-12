namespace FakeApp.Application.Albums
{
    /// <summary>
    /// Класс для маппинга данных об альбоме
    /// </summary>
    public class AlbumResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Cover { get; set; }
        public int PhotosCount { get; set; }
    }
}