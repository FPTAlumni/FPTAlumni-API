using System.ComponentModel.DataAnnotations;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.DataTier.ViewModels.Class
{
    public class GetClassDetail
    {
        public int Id { get; set; }

        [Required] public string ClassOf { get; set; }
        public int? StartYear { get; set; }
        public int? UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}