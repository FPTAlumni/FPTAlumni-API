using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.University
{
    public class UpdateUniversityRequestBody :CreateUniversityRequestBody
    {
        [Required]
        public int  Id { get; set; }
    }
}