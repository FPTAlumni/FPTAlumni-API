

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniAlumni.Business.Services.UniversityService;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.ViewModels.Token;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.Business.Services.AuthenticationService
{
    public class AuthenticationSvc : IAuthenticationSvc
    {
        private readonly IAlumniRepository _alumniRepository;
        private readonly IConfiguration _configuration;
        
        private readonly IUniversitySvc _universityService;

        public AuthenticationSvc(IAlumniRepository alumniRepository,IConfiguration configuration, IUniversitySvc universityService)
        {
            _alumniRepository = alumniRepository;
            _configuration = configuration;
            _universityService = universityService;
        }

        public async Task<TokenResponse> Authenticate(string uid)
        {
            var alumni = await LoadAlumniByUid(uid);
            // var university = await LoadUniversityById(universityId);
            if ( alumni != null && (alumni.Status == (byte?) AlumniEnum.AlumniStatus.IsAdmin || alumni.Status == (byte?) AlumniEnum.AlumniStatus.Active))
            {
                var customTokenAsync = CreateCustomToken(uid, alumni.Id, alumni.Status);
                return new TokenResponse(customTokenAsync, alumni.Id);
            }
            return new TokenResponse("",-1);
        }


        private async Task<Alumnus> LoadAlumniByUid(string uid)
        {
            return await _alumniRepository.GetFirstOrDefaultAsync(alumnus => alumnus.Uid.Equals(uid));
        }
        
        private async Task<UniversityViewModel> LoadUniversityById(int id){
            return await _universityService.GetUniversityById(id);
        }

        private string CreateCustomToken(string uid, int alumniId, byte? status)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
            Console.WriteLine(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim("id", alumniId.ToString()),
                    new Claim("uid", uid),
                    // new Claim("universityId", universityId.ToString()),
                    new Claim(ClaimTypes.Role, status == (byte?) AlumniEnum.AlumniStatus.IsAdmin ? RolesConstants.ADMIN : RolesConstants.ALUMNI)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}