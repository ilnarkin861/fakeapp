#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;



namespace FakeApp.Infrastructure.Validators
{
    /// <summary>
    /// Валидатор, проверяющий размер файла
    /// </summary>
    public class FileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;

        
        public FileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
            ErrorMessage = $"The file size cannot exceed {_maxFileSize.ToString()} Kb";
        }
        

        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            
            switch (value)
            {
                case IFormFile file:
                {
                    return file.Length <= _maxFileSize * 1000;
                }
                    
                case ICollection<IFormFile> files:
                {
                    if (files.Any())
                    {
                        if (files.Any(image => image.Length > _maxFileSize * 1000))
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