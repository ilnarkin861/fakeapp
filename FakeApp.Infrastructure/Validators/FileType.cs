#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;



namespace FakeApp.Infrastructure.Validators
{
    /// <summary>
    /// Валидатор, проверяющий тип файла
    /// </summary>
    public class FileType : ValidationAttribute
    {
        private readonly string _contentType;
        
        public FileType(string contentType)
        {
            ErrorMessage = "Incorrect file format";
            _contentType = contentType;
        }
        
        
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            
            switch (value)
            {
                case IFormFile file:
                {
                    return file.ContentType.Contains(_contentType);
                }
                    
                case ICollection<IFormFile> files:
                {
                    if (files.Any())
                    {
                        if (!files.Any(image => image.ContentType.Contains(_contentType)))
                        {
                            return false;
                        }
                    }

                    break;
                }
            }

            return true;
        }

    }
}