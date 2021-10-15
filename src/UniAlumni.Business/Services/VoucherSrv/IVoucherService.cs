using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Voucher;

namespace UniAlumni.Business.Services.VoucherSrv
{
    public interface IVoucherService
    {
        ModelsResponse<VoucherViewModel> GetVouchers(PagingParam<VoucherEnum.VoucherSortCriteria> paginationModel,
            SearchVoucherModel searchVoucherModel, bool isAdmin);
        Task<VoucherViewModel> GetVoucherById(int id, bool isAdmin);
        Task DeleteVoucher(int id);
        Task<VoucherViewModel> UpdateVoucher(VoucherUpdateRequest request);
        Task<VoucherViewModel> CreateVoucher(VoucherCreateRequest request);
    }
}
