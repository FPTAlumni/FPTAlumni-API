namespace UniAlumni.DataTier.ViewModels.Company
{
    public class BaseCompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Image { get; set; }
    }
    public class GetCompanyDetail: BaseCompanyModel
    {
        public string Location { get; set; }
        public string Business { get; set; }
        public string Description { get; set; }
    }
    public class RecruitmentCompanyModel : BaseCompanyModel
    {
        public string Location { get; set; }
    }
}