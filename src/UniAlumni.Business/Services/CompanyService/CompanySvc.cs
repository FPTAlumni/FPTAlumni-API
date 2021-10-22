using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
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

        public IList<GetCompanyDetail> GetCompanyPage(PagingParam<CompanyEnum.CompanySortCriteria> paginationModel,
            SearchCompanyModel searchCompanyModel)
        {
            IQueryable<Company> queryCompany = _companyRepository.Table;

            if (searchCompanyModel.CompanyName is {Length: > 0})
                queryCompany = queryCompany.Where(c => c.CompanyName.Contains(searchCompanyModel.CompanyName));

            if (searchCompanyModel.Business is {Length: > 0})
                queryCompany = queryCompany.Where(c => c.Business.Contains(searchCompanyModel.Business));

            if (searchCompanyModel.Location is {Length: > 0})
                queryCompany = queryCompany.Where(c => c.Location.Contains(searchCompanyModel.Location));
            // Apply Status
            queryCompany = queryCompany.Where(c => c.Status == (byte?) CompanyEnum.CompanyStatus.Active);

            IQueryable<GetCompanyDetail> companyDetails = _mapper.ProjectTo<GetCompanyDetail>(queryCompany);
            // Apply sort
            if (paginationModel.SortKey.ToString().Trim().Length > 0)
                companyDetails =
                    companyDetails.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            return companyDetails.AsEnumerable().GetWithPaging(paginationModel.Page, paginationModel.PageSize)
                .ToList();
        }

        public async Task<GetCompanyDetail> GetCompanyById(int id)
        {
            Company company = await _companyRepository.GetByIdAsync(id);
            if (company == null || company.Status == (byte?) CompanyEnum.CompanyStatus.Inactive)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Company not exist");
            }
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
            if (company == null || company.Status == (byte?) CompanyEnum.CompanyStatus.Inactive)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Company not exist");
            }
            company = _mapper.Map(requestBody, company);
            _companyRepository.Update(company);
            await _companyRepository.SaveChangesAsync();
            GetCompanyDetail companyDetail = _mapper.Map<GetCompanyDetail>(company);
            return companyDetail;
        }

        public async Task DeleteCompanyAsync(int id)
        {
            Company company = await _companyRepository.GetFirstOrDefaultAsync(alu => alu.Id == id);
            if (company == null || company.Status == (byte?) CompanyEnum.CompanyStatus.Inactive)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Company not exist");
            }
            company.Status = (byte?) CompanyEnum.CompanyStatus.Inactive;
            await _companyRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _companyRepository.GetAll().CountAsync();
        }
    }
}