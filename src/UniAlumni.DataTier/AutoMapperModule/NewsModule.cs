using AutoMapper;
using System.Linq;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.News;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class NewsModule
    {
        public static void ConfigNewsModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<News, NewsViewModel>();
            mc.CreateMap<News, NewsDetailModel>()
                .ForMember(des => des.Tags, opt => opt.MapFrom(
                src => src.TagNews.Select(tn => tn.Tag)))
                .ForMember(des => des.StringStatus, opt => opt.MapFrom(
                        src => ((NewsEnum.NewsStatus)src.Status).ToString()));
            mc.CreateMap<NewsCreateRequest, News>();
            mc.CreateMap<NewsUpdateRequest, News>();
            mc.CreateMap<News, BaseNewsModel>();
        }
    }
}
