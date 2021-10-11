using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Category
{
    public class CreateCategoryRequestBody
    {
        
        [Required]
        public string CategoryName { get; set; }
        
        public string Description { get; set; }
    }
}