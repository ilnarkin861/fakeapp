using System;



namespace FakeApp.Application.Posts
{
    /// <summary>
    /// Класс для маппинга данных о комментах к посту
    /// </summary>
    public class CommentResponse
    {
        public string Body { get; set; }
        public DateTime CommentDate { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAvatar { get; set; }
    }
}