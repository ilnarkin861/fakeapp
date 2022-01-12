using AutoMapper;
using FakeApp.Application.Albums.Commands;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Albums.Mapping
{
    /// <summary>
    /// Правила маппинга для альбомов и фоток
    /// </summary>
    public class AlbumMappingProfile : Profile
    {
        public AlbumMappingProfile()
        {
            CreateMap<Album, AlbumResponse>()
                .ForMember(v => v.UserName, opt => opt.MapFrom(v => v.User.FirstName))
                .ForMember(v => v.PhotosCount, opt => opt.MapFrom(v => v.Photos.Count));

            CreateMap<Album, UserAlbumResponse>();

            CreateMap<AlbumAddCommand, Album>();
            
            CreateMap<AlbumUpdateCommand, Album>();

            CreateMap<Photo, PhotosResponse>();
            
            CreateMap<Photo, UserPhotosResponse>();
        }
    }
}