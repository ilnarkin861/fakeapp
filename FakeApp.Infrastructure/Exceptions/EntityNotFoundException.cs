#nullable enable
using System;



namespace FakeApp.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception-класс на случай когда в бд не найдена запись
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message) : base(message)
        {
        }
    }
}