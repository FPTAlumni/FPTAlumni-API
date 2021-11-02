using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Repositories.TagRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.Business.Services.TagSrv
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IConfigurationProvider _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper.ConfigurationProvider;
        }
        public ModelsResponse<TagViewModel> GetTags(PagingParam<TagEnum.TagSortCriteria> paginationModel)
        {
            var queryTag = _tagRepository.Get(t => t.Status == (byte)TagEnum.TagStatus.Active);
            var tagViewModels = queryTag.ProjectTo<TagViewModel>(_mapper);
            tagViewModels = tagViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            var data = tagViewModels.ToList();

            return new ModelsResponse<TagViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
                Data = data,
                Metadata = new PagingMetadata()
                {
                    Page = 0,
                    Size = paginationModel.PageSize,
                    Total = data.Count
                }
            };
        }
    }
}
