using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Token
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        
        [Required]
        public int UniversityId { get; set; }
    }
}