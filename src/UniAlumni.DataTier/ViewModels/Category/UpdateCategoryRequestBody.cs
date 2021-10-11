using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Category
{
    public class UpdateCategoryRequestBody
    {
        [Required]
        public int Id { get; set;}
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}