using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Company
{
    public class UpdateCompanyRequestBody : CreateCompanyRequestBody
    {
        [Required]
        public int Id { get; set;}
    }
}