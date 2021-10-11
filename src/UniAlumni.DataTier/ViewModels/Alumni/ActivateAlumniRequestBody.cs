using System.ComponentModel.DataAnnotations;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class ActivateAlumniRequestBody
    {
        [Required]
        public int Id { get; set; }
        
        public AlumniEnum.AlumniStatus Status { get; set; }
    }
}