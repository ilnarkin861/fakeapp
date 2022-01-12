namespace FakeApp.Infrastructure.Helpers
{
    /// <summary>
    /// Класс для построения построничного вывода
    /// </summary>
    public class Paginator
    {
        private int Offset { get; }
        private int Limit { get; }
        public int Count { get; }
        public bool HasPreviousPage => Offset > 0 || Offset > Limit;
        public bool HasNextPage => Count > Offset + Limit;
        
        
        public Paginator(int count, int offset, int limit)
        {
            Count = count;
            Offset = offset;
            Limit = limit;
        }
    }
}