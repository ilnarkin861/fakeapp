using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FakeApp.Domain.Entities
{
    /// <summary>
    /// Класс модели в бд для постов
    /// </summary>
    [Table("fakeapp.posts")]
    public class Post
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}