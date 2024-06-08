using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplication11.Models;
using WebApplication12.Models.ViewModels;
namespace WebApplication12.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper mapper;           
        public static void ConfigureMapping()   
        {
            MapperConfiguration config = new MapperConfiguration(t =>
            {
                t.CreateMap<NewsGroup, NewsGroupViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<NewsGroupViewModel, NewsGroup>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<NewsViewModel, News>().IgnoreAllPropertiesWithAnInaccessibleSetter();   // az NewsViewModel beriz toye News
                t.CreateMap<News, NewsViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<Comment, CommentViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<CommentViewModel, Comment>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<User, UserViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<UserViewModel, User>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<Comment, CommentViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                t.CreateMap<CommentViewModel, Comment>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            });
            mapper = config.CreateMapper();
        }
    }
}