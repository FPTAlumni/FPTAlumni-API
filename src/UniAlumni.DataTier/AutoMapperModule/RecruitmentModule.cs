﻿using AutoMapper;
using System;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Recruitment;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class RecruitmentModule
    {
        public static void ConfigRecruitmentModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Recruitment, RecruitmentViewModel>()
               //.ForMember(des => des.Type,
               //     opt => opt.MapFrom(
               //         src => Enum.GetName(typeof(RecruitmentEnum.RecruitmentType), src.Type)))
                .ForMember(des => des.Major, opt => opt.MapFrom(
                    src => src.Group.Major))
                .ForMember(des => des.Status, opt => opt.MapFrom(
                        src => ((RecruitmentEnum.RecruitmentStatus)src.Status).ToString()))
                .ReverseMap();
            mc.CreateMap<RecruitmentCreateRequest, Recruitment>();
            mc.CreateMap<RecruitmentUpdateRequest, Recruitment>();
        }
    }
}
