using System;
using AutoMapper;
using FakeApp.Domain.Entities;
using FakeApp.Application.Posts.Commands;



namespace FakeApp.Application.Posts.Mapping
{
    /// <summary>
    /// Правила маппинга для постов
    /// </summary>
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostResponse>()
                .ForMember(v => v.UserName, opt => opt.MapFrom(v => v.User.FirstName))
                .ForMember(v => v.UserAvatar, opt => opt.MapFrom(v => v.User.Avatar))
                .ForMember(v => v.CommentsCount, opt => opt.MapFrom(v => v.Comments.Count));

            CreateMap<Post, UserPostResponse>()
                .ForMember(v => v.CommentsCount, opt => opt.MapFrom(v => v.Comments.Count));
            
            CreateMap<PostAddCommand, Post>()
                .ForMember(v => v.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now));
            
            CreateMap<PostUpdateCommand, Post>();
        }

    }
}