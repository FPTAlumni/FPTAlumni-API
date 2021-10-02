using System.ComponentModel;

namespace UniAlumni.DataTier.ViewModels.Company
{
    public class SearchCompanyModel
    {
        [DefaultValue("")]
        public string CompanyName { get; set; } = "";
        
        [DefaultValue("")]
        public string Location { get; set; } = "";
        
        [DefaultValue("")]
        public string Business { get; set; } = "";
    }
}