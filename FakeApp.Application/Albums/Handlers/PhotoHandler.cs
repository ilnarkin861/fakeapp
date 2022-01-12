using FakeApp.Application.Albums.Services;
using FakeApp.Application.Services;



namespace FakeApp.Application.Albums.Handlers
{
    /// <summary>
    /// Базовый handler-класс для фоток
    /// </summary>
    public abstract class PhotoHandler
    {
        protected readonly IPhotosService PhotosService;
        protected readonly IHttpService HttpService;


        protected PhotoHandler(IPhotosService photosService, IHttpService httpService)
        {
            PhotosService = photosService;
            HttpService = httpService;
        }
    }
}