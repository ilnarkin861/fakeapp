using Microsoft.AspNetCore.Http;



namespace FakeApp.Application.Services
{
    /// <summary>
    /// Интерфейс для классов, работающих с http-контекстом
    /// </summary>
    public interface IHttpService
    {
        HttpContext GetHttpContext();
        string GetBaseUrl();

    }
}