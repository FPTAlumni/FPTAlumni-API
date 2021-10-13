using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Voucher;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class VoucherModule
    {
        public static void ConfigVoucherModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Voucher, VoucherViewModel>()
                .ForMember(des => des.Status, opt => opt.MapFrom(
                    src => ((VoucherEnum.VoucherStatus)src.Status).ToString()));
            mc.CreateMap<VoucherCreateRequest, Voucher>();
            mc.CreateMap<VoucherUpdateRequest, Voucher>();
            mc.CreateMap<Voucher, BaseVoucherModel>();
        }
    }
}
