using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FakeApp.Infrastructure.Options;



namespace FakeApp.Infrastructure.Services.FileSystem
{
    /// <summary>
    /// Интерфейс для классов, работающих с файловой системой
    /// </summary>
    public interface IFileSystemService
    {
        string CreateDir(string relativePath);
        string CreateDir(string relativePath, string dirName);
        Task<string> UploadFileAsync(string uploadDir, IFormFile file);
        bool DeleteFile(string filePath);
        MediaOptions GetMediaOptions();
        bool FileExists(string filePath);
        bool DirectoryExists(string path);
    }
}