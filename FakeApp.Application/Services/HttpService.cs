using Microsoft.AspNetCore.Http;



namespace FakeApp.Application.Services
{
    /// <summary>
    /// Класс, работающий с http-контекстом
    /// </summary>
    public class HttpService : IHttpService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        
        public HttpService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public HttpContext GetHttpContext()
        {
            return _contextAccessor.HttpContext;
        }

        
        public string GetBaseUrl()
        {
            var request = _contextAccessor.HttpContext.Request;

            var port = request.Host.Port.HasValue ? $":{request.Host.Port.ToString()}" : string.Empty;

            return $"{request.Scheme}://{request.Host.Host}{port}";
        }
    }
}