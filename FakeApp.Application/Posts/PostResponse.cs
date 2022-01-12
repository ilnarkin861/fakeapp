using System;



namespace FakeApp.Application.Posts
{
    /// <summary>
    /// Класс для маппинга данных о посте
    /// </summary>
    public class PostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string Image { get; set; }
        public int CommentsCount { get; set; }
    }
}