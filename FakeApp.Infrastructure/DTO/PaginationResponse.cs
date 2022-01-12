using System.Collections;
using FakeApp.Infrastructure.Helpers;



namespace FakeApp.Infrastructure.DTO
{
    /// <summary>
    /// Класс для маппинга данных с пагинацией
    /// </summary>
    public class PaginationResponse
    {
        public ICollection Data { get; set; }
        public Paginator Pagination { get; set; }
    }
}