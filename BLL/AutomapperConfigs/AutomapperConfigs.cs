using AutoMapper;
using BLL.DTO;
using BLL.DTO.Filters;
using DAL.Entities;
using DAL.Entities.Filters;

namespace BLL.AutomapperConfigs
{
    public static class AutomapperConfigs
    {
        public static IMapper GetBasicMapper<TDto, TEntity>()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, TDto>();
                cfg.CreateMap<TDto, TEntity>();
            }).CreateMapper();
        }

        public static IMapper GetAccountServiceMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDTO>();
                cfg.CreateMap<AuthorDTO, Author>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            }).CreateMapper();
        }

        public static IMapper GetUserServiceMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDTO>();
                cfg.CreateMap<AuthorDTO, Author>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            }).CreateMapper();
        }

        public static IMapper GetPostServiceMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostDTO, Post>();
                cfg.CreateMap<PostFilterDTO, PostFilter>();
            }).CreateMapper();
        }
    }
}
