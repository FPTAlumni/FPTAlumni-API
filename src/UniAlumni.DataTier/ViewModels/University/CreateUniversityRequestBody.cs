using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.University
{
    public class CreateUniversityRequestBody 
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
    }
}