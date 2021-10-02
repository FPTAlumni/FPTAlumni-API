using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.CompanyRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Company;

namespace UniAlumni.Business.Services.CompanyService
{
    public class CompanySvc : ICompanySvc
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;


        public CompanySvc(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public IList<GetCompanyDetail> GetCompanyPage(PagingParam<CompanyEnum.CompanySortCriteria> paginationModel, SearchCompanyModel searchCompanyModel)
        {
            IQueryable<Company> queryCompany = _companyRepository.Table;

            if (searchCompanyModel.CompanyName.Length > 0 || 
                searchCompanyModel.Business.Length > 0 ||
                searchCompanyModel.Location.Length > 0)
            {
                queryCompany = queryCompany.Where(c => c.CompanyName.Contains(searchCompanyModel.CompanyName) &&
                                                         c.Business.Contains(searchCompanyModel.Business) &&
                                                                             c.Location.Contains(searchCompanyModel.Location));
            }

            queryCompany = queryCompany.Where(c => c.Status == (byte?) CompanyEnum.CompanyStatus.Active);

            IQueryable<GetCompanyDetail> companyDetails = _mapper.ProjectTo<GetCompanyDetail>(queryCompany);
            // Apply sort
            companyDetails =
                companyDetails.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            return companyDetails.AsEnumerable().GetWithPaging(paginationModel.Page, paginationModel.PageSize)
                .ToList();
        }

        public async Task<GetCompanyDetail> GetCompanyById(int id)
        {
            Company company = await _companyRepository.GetByIdAsync(id);
            GetCompanyDetail companyDetail = _mapper.Map<GetCompanyDetail>(company);
            return companyDetail;
        }

        public async Task<GetCompanyDetail> CreateCompanyAsync(CreateCompanyRequestBody requestBody)
        {
            Company company = _mapper.Map<Company>(requestBody);
            company.Status = (byte?) CompanyEnum.CompanyStatus.Active;

            await _companyRepository.InsertAsync(company);
            await _companyRepository.SaveChangesAsync();

            GetCompanyDetail companyDetail = _mapper.Map<GetCompanyDetail>(company);
            return companyDetail;
        }

        public async Task<GetCompanyDetail> UpdateCompanyAsync(UpdateCompanyRequestBody requestBody)
        {
            Company company = await _companyRepository.GetFirstOrDefaultAsync(alu => alu.Id == requestBody.Id);
            company = _mapper.Map(requestBody, company);
            _companyRepository.Update(company);
            await _companyRepository.SaveChangesAsync();
            GetCompanyDetail companyDetail = _mapper.Map<GetCompanyDetail>(company);
            return companyDetail;
        }

        public async Task DeleteCompanyAsync(int id)
        {
            Company company = await _companyRepository.GetFirstOrDefaultAsync(alu => alu.Id == id);
            company.Status = (byte?) CompanyEnum.CompanyStatus.Inactive;
            await _companyRepository.SaveChangesAsync();
        }
    }
}