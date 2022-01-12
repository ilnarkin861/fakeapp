using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using FakeApp.Infrastructure.Options;
using FakeApp.Infrastructure.Services.FileSystem;
using FakeApp.Infrastructure.Validators;



namespace FakeApp.Test
{
    public class CustomValidatorsTest : AppTest
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
        public async Task FileSizeValidTest()
        {
            var fileSizeValidator = new FileSize(10000000);

            Assert.True(fileSizeValidator.IsValid(await GetFileObject()));
        }
        
        
        [Test]
        public async Task FileSizeNotValidTest()
        {
            var fileSizeValidator = new FileSize(1000);

            Assert.False(fileSizeValidator.IsValid(await GetFileObject()));
        }


        [Test]
        public async Task FileTypeNotValidTest()
        {
            var fileTypeValidator = new FileType("json");
            
            Assert.False(fileTypeValidator.IsValid(await GetFileObject()));
        }
        
        
        [Test]
        public async Task FileTypeValidTest()
        {
            var fileTypeValidator = new FileType("image");
            
            Assert.True(fileTypeValidator.IsValid(await GetFileObject()));
        }


        [Test]
        public void CollectionsNotNullOrNotEmptyNotValidTest()
        {
            var ensureOneElementValidator = new EnsureOneElement();
            
            Assert.False(ensureOneElementValidator.IsValid(new List<object>()));
            
            Assert.False(ensureOneElementValidator.IsValid(null));
        }


        [Test]
        public void CollectionsNotNullOrNotEmptyValidTest()
        {
            var list = new List<object>
            {
                new(),
                new(),
                new()
            };
            
            var ensureOneElementValidator = new EnsureOneElement();
            
            Assert.True(ensureOneElementValidator.IsValid(list));
        }


        private async Task<IFormFile> GetFileObject()
        {
            const string testFile = "big_image.jpg";
            
            var filePath = Path.Combine(_options.MediaRootPath, testFile);
            
            await using var fs = new FileStream(filePath, FileMode.Open);

            var formFile = new FormFile(fs, 0, fs.Length, "test_file", testFile)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var httpContext = new DefaultHttpContext();
            
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");

            var filesCollection = new FormFileCollection {formFile};
            
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), filesCollection);
            
            return httpContext.Request.Form.Files[0];
        }
    }
}