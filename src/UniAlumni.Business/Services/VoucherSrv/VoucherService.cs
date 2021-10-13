using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.VoucherRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Voucher;

namespace UniAlumni.Business.Services.VoucherSrv
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _repository;
        private readonly IConfigurationProvider _mapper;

        public VoucherService(IVoucherRepository repository, IMapper mapper)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
        }
        public async Task<VoucherViewModel> GetVoucherById(int id)
        {
            var voucherModel = await _repository.Get(g => g.Id == id).ProjectTo<VoucherViewModel>(_mapper).FirstOrDefaultAsync();
            return voucherModel;
        }

        public ModelsResponse<VoucherViewModel> GetVouchers(PagingParam<VoucherEnum.VoucherSortCriteria> paginationModel, SearchVoucherModel searchVoucherModel)
        {
            var vouchersQuery = _repository.GetAll();
            if (searchVoucherModel.MajorId != null)
                vouchersQuery = vouchersQuery.Where(v => v.MajorId == searchVoucherModel.MajorId);
            if (searchVoucherModel.Status != null)
                vouchersQuery = vouchersQuery.Where(v => v.Status == (byte?)searchVoucherModel.Status);

            var viewModels = vouchersQuery.ProjectTo<VoucherViewModel>(_mapper);
            viewModels = viewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            var data = viewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<VoucherViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = data,
                Metadata = new PagingMetadata()
                {
                    Page = paginationModel.Page,
                    Size = paginationModel.PageSize,
                    Total = data.Count
                }
            };
        }
        public async Task<VoucherViewModel> CreateVoucher(VoucherCreateRequest request)
        {
            var mapper = _mapper.CreateMapper();
            var voucher = mapper.Map<Voucher>(request);
            voucher.Status = (byte)ReferralEnum.ReferralStatus.Active;

            _repository.Insert(voucher);
            await _repository.SaveChangesAsync();
            return await _repository.Get(r => r.Id == voucher.Id).ProjectTo<VoucherViewModel>(_mapper).FirstOrDefaultAsync();
        }

        public async Task DeleteVoucher(int id)
        {
            var voucher = await _repository.GetFirstOrDefaultAsync(r => r.Id == id);
            if (voucher != null)
            {
                voucher.Status = (int)VoucherEnum.VoucherStatus.Inactive;
                voucher.UpdatedDate = DateTime.Now;
                _repository.Update(voucher);
                await _repository.SaveChangesAsync();
            }
        }
        public async Task<VoucherViewModel> UpdateVoucher(VoucherUpdateRequest request)
        {
            var voucher = await _repository.GetFirstOrDefaultAsync(r => r.Id == request.Id);

            if (voucher != null)
            {
                var mapper = _mapper.CreateMapper();
                voucher = mapper.Map(request, voucher);
                voucher.UpdatedDate = DateTime.Now;
                _repository.Update(voucher);
                await _repository.SaveChangesAsync();
                return await _repository.Get(r => r.Id == voucher.Id).ProjectTo<VoucherViewModel>(_mapper).FirstOrDefaultAsync();
            }
            return null;
        }
    }
}
