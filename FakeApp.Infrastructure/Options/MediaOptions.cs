namespace FakeApp.Infrastructure.Options
{
    /// <summary>
    /// Класс для данных файловой системы
    /// </summary>
    public class MediaOptions
    {
        public string MediaRootPath { get; set; }
        public string ImagesDir { get; set; }
        public string AvatarsDir { get; set; }
        public string PostImagesDir { get; set; }
        public string PhotosDir { get; set; }
        public string AlbumCoversDir { get; set; }
    }
}