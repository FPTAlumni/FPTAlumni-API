using System.ComponentModel.DataAnnotations;
using UniAlumni.DataTier.Object;

namespace UniAlumni.WebAPI.DTO.Token
{
    public class TokenResponse
    {
        /// <summary>
        /// The Custom Token of User
        /// </summary>
        /// <example>ey...</example>
        [Required]
        public string CustomToken { get; set; }

        // [Required]
        // public string Role { get; set; }
        //
        // public int ?GroupId { get; set; }
        public static TokenResponse BuildTokenResponse(string token) => new TokenResponse {CustomToken = token};

    }
}