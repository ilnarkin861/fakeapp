using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MediatR;
using FakeApp.Infrastructure.Validators;



namespace FakeApp.Application.Albums.Commands
{
    /// <summary>
    /// Класс для биндинга данных на запрос загрузки фоток
    /// </summary>
    public class PhotosUploadCommand : IRequest<ICollection<UserPhotosResponse>>
    {
        [Required(ErrorMessage = "Album id is required")]
        public int AlbumId { get; set; }
        
        [Required(ErrorMessage = "Photos is required")]
        [FileSize(1000)]
        [FileType("image")]
        [EnsureOneElement]
        public ICollection<IFormFile> Photos { get; set; }
    }
}