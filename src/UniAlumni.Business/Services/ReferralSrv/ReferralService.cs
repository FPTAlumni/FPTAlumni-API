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
using UniAlumni.DataTier.Repositories.ReferralRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Referral;

namespace UniAlumni.Business.Services.ReferralSrv
{
    public class ReferralService : IReferralService
    {
        private readonly IReferralRepository _repository;
        private readonly IConfigurationProvider _mapper;

        public ReferralService(IReferralRepository repository, IMapper mapper)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
        }
        public async Task<ReferralViewModel> CreateReferral(ReferralCreateRequest request)
        {
            var mapper = _mapper.CreateMapper();
            var referral = mapper.Map<Referral>(request);
            referral.Status = (byte)ReferralEnum.ReferralStatus.Active;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var voucherCode = new string(
                Enumerable.Repeat(chars, 10)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            referral.VoucherCode = voucherCode;
            _repository.Insert(referral);
            await _repository.SaveChangesAsync();
            return await _repository.Get(r => r.Id == referral.Id).ProjectTo<ReferralViewModel>(_mapper).FirstOrDefaultAsync();
        }

        public async Task DeleteReferral(int id, int userId, bool isAdmin)
        {
            var referral = await _repository.GetFirstOrDefaultAsync(r => r.Id == id);
            if (referral != null)
            {
                if (userId == referral.NominatorId || isAdmin)
                {
                    referral.Status = (int)ReferralEnum.ReferralStatus.Inactive;
                    referral.UpdatedDate = DateTime.Now;
                    _repository.Update(referral);
                    await _repository.SaveChangesAsync();
                }
            }
        }

        public async Task<ReferralViewModel> GetReferralById(int id, int userId, bool isAdmin)
        {
            var referralModel = await _repository.Get(r => r.Id == id)
                .ProjectTo<ReferralViewModel>(_mapper).FirstOrDefaultAsync();
            if (userId == referralModel.Nominator.Id || isAdmin)
                return referralModel;
            else return null;
        }

        public ModelsResponse<ReferralViewModel> GetReferrals(PagingParam<ReferralEnum.ReferralSortCriteria> paginationModel,
            SearchReferralModel searchReferralModel, int userId, bool isAdmin)
        {
            var referralQuery = _repository.GetAll();
            if (!isAdmin)
            {
                referralQuery.Where(r => r.NominatorId == userId);
            }
            else
            {
                if (searchReferralModel.NominatorId != null)
                    referralQuery = referralQuery.Where(r => r.NominatorId == searchReferralModel.NominatorId);
            }

            if (searchReferralModel.MajorId != null)
                referralQuery = referralQuery.Where(r => r.Nominator.ClassMajor.MajorId == searchReferralModel.MajorId);
            if (searchReferralModel.Status != null)
                referralQuery = referralQuery.Where(r => r.Status == (byte?)searchReferralModel.Status);


            var viewModels = referralQuery.ProjectTo<ReferralViewModel>(_mapper);
            viewModels = viewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            var data = viewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<ReferralViewModel>()
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

        public async Task<ReferralViewModel> UpdateReferral(ReferralUpdateRequest request)
        {
            var referral = await _repository.GetFirstOrDefaultAsync(r => r.Id == request.Id);

            if (referral != null)
            {
                var mapper = _mapper.CreateMapper();
                referral = mapper.Map(request, referral);
                referral.UpdatedDate = DateTime.Now;
                _repository.Update(referral);
                await _repository.SaveChangesAsync();
                return await _repository.Get(r => r.Id == referral.Id).ProjectTo<ReferralViewModel>(_mapper).FirstOrDefaultAsync();
            }
            return null;
        }
    }
}
