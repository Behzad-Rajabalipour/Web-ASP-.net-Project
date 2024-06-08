using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication12.Models.ViewModels;
using WebApplication11.Models;
using WebApplication12.App_Start;

namespace WebApplication12.Classes
{
    // Extension class ha, ham class ham method static hastan
    public static class ExtensionClass
    {
        public static NewsViewModel NewsToNewsViewModel(News news)
        {
            return AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
        }

        public static List<NewsViewModel> LNewsToLNewsViewModel(IEnumerable<News> news)
        {
            return AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(news);
        }

        public static List<NewsGroupViewModel> IENewsGroupToLNewsGroupViewModel(IEnumerable<NewsGroup> newsGroups)
        {
            return AutoMapperConfig.mapper.Map<IEnumerable<NewsGroup>, List<NewsGroupViewModel>>(newsGroups);
        }
    }
}