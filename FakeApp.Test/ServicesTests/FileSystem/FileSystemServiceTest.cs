using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using FakeApp.Infrastructure.Options;
using FakeApp.Infrastructure.Services.FileSystem;


namespace FakeApp.Test.ServicesTests.FileSystem
{
    public class FileSystemServiceTest : AppTest
    {
        private IFileSystemService _fileSystemService;
        private MediaOptions _options;
        
        
        [SetUp]
        public void SetUp()
        {
            _fileSystemService = GetFileSystemService();

            _options = _fileSystemService.GetMediaOptions();
        }


        [Test]
        public void CreatingDirTest()
        {
            const string dirName = "testdir";

            var dirNameDefault = _fileSystemService.CreateDir(_options.ImagesDir);

            var userDirName = _fileSystemService.CreateDir(_options.ImagesDir, dirName);
            
            Assert.True(_fileSystemService.DirectoryExists(Path.Combine(_options.ImagesDir, dirNameDefault)));
            
            Assert.True(_fileSystemService.DirectoryExists(Path.Combine(_options.ImagesDir, userDirName)));
        }


        [Test]
        public async Task UploadFileTest()
        {
            var fileName = await UploadFile(_options.ImagesDir);
            
            Assert.True(_fileSystemService.FileExists(Path.Combine(_options.ImagesDir, fileName)));
        }


        [Test]
        public async Task DeleteFileTest()
        {
            var fileName = await UploadFile(_options.ImagesDir);

            Assert.True(_fileSystemService.FileExists(Path.Combine(_options.ImagesDir, fileName)));

            _fileSystemService.DeleteFile(Path.Combine(_options.ImagesDir, fileName));
            
            Assert.False(_fileSystemService.FileExists(Path.Combine(_options.ImagesDir, fileName)));
        }


        private async Task<string> UploadFile(string path)
        {
            const string testImageFile = "test.jpeg";

            var filePath = Path.Combine(_options.MediaRootPath, testImageFile);

            await using var fs = new FileStream(filePath, FileMode.Open);
            
            var formFile = new FormFile(fs, 0, fs.Length, "test", testImageFile);
                
            return await _fileSystemService.UploadFileAsync(path, formFile);
        }
    }
}