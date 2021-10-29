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
using UniAlumni.DataTier.Common.Exception;
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
        public async Task<VoucherViewModel> GetVoucherById(int id, bool isAdmin)
        {
            var voucherQuery = _repository.Get(g => g.Id == id);
            if (!isAdmin)
                voucherQuery = voucherQuery.Where(v => v.Status == (byte)VoucherEnum.VoucherStatus.Active);

            var voucherModel = await voucherQuery.ProjectTo<VoucherViewModel>(_mapper).FirstOrDefaultAsync();
            if (voucherModel == null)
                throw new MyHttpException(StatusCodes.Status404NotFound, "Cannot find matching voucher");
            return voucherModel;
        }

        public ModelsResponse<VoucherViewModel> GetVouchers(PagingParam<VoucherEnum.VoucherSortCriteria> paginationModel,
            SearchVoucherModel searchVoucherModel, bool isAdmin)
        {
            var vouchersQuery = _repository.GetAll();
            if (!isAdmin)
            {
                vouchersQuery = vouchersQuery.Where(v => v.Status == (byte?)VoucherEnum.VoucherStatus.Active);
            }
            else if (searchVoucherModel.Status != null)
                vouchersQuery = vouchersQuery.Where(v => v.Status == (byte?)searchVoucherModel.Status);
            if (searchVoucherModel.MajorId != null)
                vouchersQuery = vouchersQuery.Where(v => v.MajorId == searchVoucherModel.MajorId);


            var viewModels = vouchersQuery.ProjectTo<VoucherViewModel>(_mapper);
            viewModels = viewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            var data = viewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<VoucherViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
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
            voucher.Status = (byte)VoucherEnum.VoucherStatus.Active;

            _repository.Insert(voucher);
            await _repository.SaveChangesAsync();
            return await _repository.Get(r => r.Id == voucher.Id).ProjectTo<VoucherViewModel>(_mapper).FirstOrDefaultAsync();
        }

        public async Task DeleteVoucher(int id)
        {
            var voucher = await _repository.GetFirstOrDefaultAsync(r => r.Id == id);
            if (voucher == null)
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching voucher");

            voucher.Status = (int)VoucherEnum.VoucherStatus.Inactive;
            voucher.UpdatedDate = DateTime.Now;
            _repository.Update(voucher);
            await _repository.SaveChangesAsync();

        }
        public async Task<VoucherViewModel> UpdateVoucher(VoucherUpdateRequest request)
        {
            var voucher = await _repository.GetFirstOrDefaultAsync(r => r.Id == request.Id);

            if (voucher == null)
            {
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching voucher");
            }
            var mapper = _mapper.CreateMapper();
            voucher = mapper.Map(request, voucher);
            voucher.UpdatedDate = DateTime.Now;
            _repository.Update(voucher);
            await _repository.SaveChangesAsync();
            return await _repository.Get(r => r.Id == voucher.Id).ProjectTo<VoucherViewModel>(_mapper).FirstOrDefaultAsync();

        }
    }
}
