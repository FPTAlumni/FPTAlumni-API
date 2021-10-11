using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Class
{
    public class CreateClassRequestBody 
    {
        [Required]
        public string ClassOf { get; set; }
        public int? StartYear { get; set; }
    }
}