using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.Business.Services.TagSrv
{
    public interface ITagService
    {
        ModelsResponse<TagViewModel> GetTags(PagingParam<TagEnum.TagSortCriteria> paginationModel);

    }
}
