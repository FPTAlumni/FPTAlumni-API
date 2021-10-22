using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class SearchAlumniModel
    {
        [DefaultValue("")] public string Email { get; set; } = "";
        
        [DefaultValue("")] public string Phone { get; set; } = "";
        
        [DefaultValue("")]
        [FromQuery(Name = "fullname")]
        public string FullName { get; set; } = "";
        
        [DefaultValue("")] public string Uid { get; set; } = "";

        [DefaultValue("")] public int? GroupId { get; set; } = null;
        
        [DefaultValue("")] public int? UniversityId { get; set; } = null;

        [DefaultValue("")] public int? EventId { get; set; } = null;
        
        
        public AlumniEnum.AlumniStatus? Status { get; set; } = null;
    }
}