using System.ComponentModel.DataAnnotations;

namespace UniAlumni.WebAPI.DTO.Token
{
    public class TokenResponse
    {
        [Required]
        public string CustomToken { get; set; }
        public static TokenResponse BuildTokenResponse(string token) => new TokenResponse {CustomToken = token};

    }
}