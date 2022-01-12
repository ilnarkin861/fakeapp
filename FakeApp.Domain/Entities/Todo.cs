using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FakeApp.Domain.Entities
{
    /// <summary>
    /// Класс модели в бд для задач
    /// </summary>
    [Table("fakeapp.todos")]
    public class Todo
    {
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }
        public bool Completed { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public User User { get; set; }
    }
}