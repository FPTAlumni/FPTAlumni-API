using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace UniAlumni.DataTier.ViewModels.Company
{
    public class SearchCompanyModel
    {
        [DefaultValue("")]
        [FromQuery(Name = "company-name")]
        public string CompanyName { get; set; } = "";
        
        [DefaultValue("")]
        public string Location { get; set; } = "";
        
        [DefaultValue("")]
        public string Business { get; set; } = "";
    }
}