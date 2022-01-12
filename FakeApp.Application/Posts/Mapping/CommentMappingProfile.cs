using System;
using AutoMapper;
using FakeApp.Application.Posts.Commands;
using FakeApp.Domain.Entities;



namespace FakeApp.Application.Posts.Mapping
{
    /// <summary>
    /// Правила маппинга для комментов
    /// </summary>
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentResponse>()
                .ForMember(v => v.UserFirstName, opt => opt.MapFrom(v => v.User.FirstName))
                .ForMember(v => v.UserLastName, opt => opt.MapFrom(v => v.User.LastName))
                .ForMember(v => v.UserAvatar, opt => opt.MapFrom(v => v.User.Avatar));

            CreateMap<CommentAddCommand, Comment>()
                .ForMember(v => v.CommentDate, opt => opt.MapFrom(_ => DateTime.Now));
        }
    }
}