﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Referral;

namespace UniAlumni.Business.Services.ReferralSrv
{
    public interface IReferralService
    {
        ModelsResponse<ReferralViewModel> GetReferrals(PagingParam<ReferralEnum.ReferralSortCriteria> paginationModel,
            SearchReferralModel searchReferralModel, int userId, bool isAdmin);
        Task<ReferralViewModel> GetReferralById(int id, int userId, bool isAdmin);
        Task<ReferralViewModel> CreateReferral(ReferralCreateRequest request);
        Task<ReferralViewModel> UpdateReferral(ReferralUpdateRequest request);
        Task DeleteReferral(int id, int userId, bool isAdmin);
    }
}
