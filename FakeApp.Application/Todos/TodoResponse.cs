namespace FakeApp.Application.Todos
{
    public class TodoResponse
    {
        /// <summary>
        /// Класс для маппинга данных о задаче юзера
        /// </summary>
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
    }
}