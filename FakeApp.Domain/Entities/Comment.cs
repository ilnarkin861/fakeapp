using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FakeApp.Domain.Entities
{
    /// <summary>
    /// Класс модели в бд для комментов к постам
    /// </summary>
    [Table("fakeapp.comments")]
    public class Comment
    {
        public int Id { get; set; }
        
        [Required]
        public string Body { get; set; }
        public DateTime CommentDate { get; set; }
        
        [Required]
        public int PostId { get; set; }
        
        [Required]
        public Post Post { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public User User { get; set; }
    }
}