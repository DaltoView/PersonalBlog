using AutoMapper;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.Models.AccountController;
using WebApi.Models.CommentsController;
using WebApi.Models.PostsController;
using WebApi.Models.UsersController;

namespace WebApi.AutomapperConfigs
{
    public static class AutomapperConfigs
    {
        public static IMapper GetBasic<TModel, TDto>()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModel, TDto>();
                cfg.CreateMap<TDto, TModel>();
            }).CreateMapper();
        }

        public static IMapper GetAccountControllerMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterModel, AuthorDTO>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(p => p.Birthdate, o => o.MapFrom(s => s.Birthdate));

                cfg.CreateMap<UserRegisterModel, UserDTO>()
                .ForMember(p => p.Author, o => o.MapFrom(s => s));

                cfg.CreateMap<UserDTO, UserAccountModel>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.Author.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.Author.LastName))
                .ForMember(p => p.Birthdate, o => o.MapFrom(s => s.Author.Birthdate));

                cfg.CreateMap<UserAccountModel, UserDTO>()
                .ForMember(p => p.Author, o => o.MapFrom(s => s))
                .ForMember(p => p.Password, o => o.Ignore());

                cfg.CreateMap<UserAccountModel, AuthorDTO>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(p => p.Birthdate, o => o.MapFrom(s => s.Birthdate))
                .ForMember(p => p.Id, o => o.Ignore());
            }).CreateMapper();
        }

        public static IMapper GetUsersControllerMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateModel, UserDTO>()
                .ForMember(p => p.Author, o => o.MapFrom(s => s))
                .ForMember(p => p.Id, o => o.Ignore());

                cfg.CreateMap<UserCreateModel, AuthorDTO>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(p => p.Birthdate, o => o.MapFrom(s => s.Birthdate))
                .ForMember(p => p.Id, o => o.Ignore());

                cfg.CreateMap<UserDTO, UserModel>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.Author.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.Author.LastName))
                .ForMember(p => p.Birthdate, o => o.MapFrom(s => s.Author.Birthdate));


                cfg.CreateMap<UserModel, AuthorDTO>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(p => p.Birthdate, o => o.MapFrom(s => s.Birthdate));

                cfg.CreateMap<UserModel, UserDTO>()
                .ForMember(p => p.Author, o => o.MapFrom(s => s));
            }).CreateMapper();
        }

        public static IMapper GetPostsControllerMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TagModel, TagDTO>();
                cfg.CreateMap<PostCreateModel, PostDTO>();
                cfg.CreateMap<TagDTO, TagModel>();
                cfg.CreateMap<PostDTO, PostModel>()
                .ForMember(p => p.FullName, o => o.MapFrom(s => string.Join(" ", s.Author.FirstName, s.Author.LastName)));

                cfg.CreateMap<CommentDTO, CommentModel>();
                cfg.CreateMap<CommentModel, CommentDTO>()
                .ForMember(p => p.Author, o => o.Ignore())
                .ForMember(p => p.Post, o => o.Ignore());
            }).CreateMapper();
        }

        public static IMapper GetCommentsControllerMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommentEditModel, CommentDTO>()
                .ForMember(p => p.AuthorId, o => o.Ignore())
                .ForMember(p => p.EditDate, o => o.Ignore())
                .ForMember(p => p.PostDate, o => o.Ignore())
                .ForMember(p => p.PostId, o => o.Ignore())
                .ForMember(p => p.Author, o => o.Ignore())
                .ForMember(p => p.Post, o => o.Ignore());

                cfg.CreateMap<CommentModel, CommentDTO>()
                .ForMember(p => p.Author, o => o.Ignore())
                .ForMember(p => p.Post, o => o.Ignore());
            }).CreateMapper();
        }
    }
}