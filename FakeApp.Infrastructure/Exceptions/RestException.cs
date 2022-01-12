#nullable enable
using System;



namespace FakeApp.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception-класс, обрабатывающий ошибки, которые могут возникнуть при работе приложения
    /// </summary>
    public class RestException : Exception
    {
        public RestException(string? message) : base(message)
        {
        }
    }
}