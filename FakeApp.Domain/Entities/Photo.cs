using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FakeApp.Domain.Entities
{
    /// <summary>
    /// Класс модели в бд для фоток к альбому
    /// </summary>
    [Table("fakeapp.photos")]
    public class Photo
    {
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; }

        [Required]
        public int AlbumId { get; set; }
        
        [Required]
        public Album Album { get; set; }
    }
}