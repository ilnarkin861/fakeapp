using System;



namespace FakeApp.Application.Posts
{
    /// <summary>
    /// Класс для маппинга данных о посте для текущего авторизованного юзера
    /// </summary>
    public class UserPostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public int CommentsCount { get; set; }
    }
}