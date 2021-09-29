using System.ComponentModel;
using System.Xml.Serialization;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class SearchAlumniModel
    {
        [DefaultValue("")]
        public string Email { get; set; } = "";
        
        [DefaultValue("")]
        public string Phone { get; set; } = "";
        
        [DefaultValue("")]
        public string FullName { get; set; } = "";
        
        [DefaultValue("")]
        public string Uid { get; set; } = "";
        
       
        [DefaultValue(AlumniEnum.AlumniStatus.Active)]
        public AlumniEnum.AlumniStatus? Status { get; set; } =  AlumniEnum.AlumniStatus.Active;
    }
}