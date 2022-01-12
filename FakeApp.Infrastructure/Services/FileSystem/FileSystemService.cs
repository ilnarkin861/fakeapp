using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using FakeApp.Infrastructure.Options;



namespace FakeApp.Infrastructure.Services.FileSystem
{
    /// <summary>
    /// Класс, работающий с файловой системой
    /// </summary>
    public class FileSystemService : IFileSystemService
    {
        private readonly MediaOptions _mediaOptions;

        
        public FileSystemService(IOptions<MediaOptions> options)
        {
            _mediaOptions = options.Value;
        }
        
        public string CreateDir(string path)
        {
            var dirName = $"{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString()}{DateTime.Now.Day.ToString()}";

            return CreateDir(path, dirName);
        }
        
        public string CreateDir(string path, string dirName)
        {
            var dirFullPath = Path.Combine(_mediaOptions.MediaRootPath, path, dirName);
           
            if (!Directory.Exists(dirFullPath))
                Directory.CreateDirectory(dirFullPath);

            return dirName;
        }
        
        
        public async Task<string> UploadFileAsync(string uploadDir, IFormFile file)
        {
            var newFileName = GetFileNameFromGuid(file.FileName);

            var uploadableFile = Path.Combine(_mediaOptions.MediaRootPath, uploadDir, newFileName);

            using (var stream = File.Create(uploadableFile))
            {
                await file.CopyToAsync(stream);
            }

            return newFileName;
        }
        

        public bool DeleteFile(string filePath)
        {
            var path = Path.Combine(_mediaOptions.MediaRootPath, filePath);
            
            if(File.Exists(path)) File.Delete(path);

            return !File.Exists(path);
        }


        public MediaOptions GetMediaOptions()
        {
            return _mediaOptions;
        }
        

        public bool FileExists(string filePath)
        {
            return File.Exists(Path.Combine(_mediaOptions.MediaRootPath, filePath));
        }
        

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(Path.Combine(_mediaOptions.MediaRootPath, path));
        }


        private string GetFileNameFromGuid(string fileName)
        {
            return $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}".Replace("-", "");
        }
    }
}