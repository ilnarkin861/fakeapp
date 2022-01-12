using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FakeApp.Domain.Entities
{
    /// <summary>
    /// Класс модели в бд для альбомов
    /// </summary>
    [Table("fakeapp.albums")]
    public class Album
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Cover { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public User User { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}