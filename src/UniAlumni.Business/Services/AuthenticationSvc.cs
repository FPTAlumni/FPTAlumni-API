using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniAlumni.Business.Services.Interface;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.Repositories;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UniAlumni.Business.Services
{
    public class AuthenticationSvc : IAuthenticationSvc
    {
        private readonly IAlumniRepository _alumniRepository;
        private readonly FirebaseAuth _firebaseAuth;
        private readonly IConfiguration _configuration;

        public AuthenticationSvc(IAlumniRepository alumniRepository,IConfiguration configuration)
        {
            _alumniRepository = alumniRepository;
            _configuration = configuration;
            _firebaseAuth = FirebaseAuth.DefaultInstance;
        }

        public string Authenticate(string uid)
        {
            Console.WriteLine(uid);
            var alumni =  LoadAlumniByUid(uid);
            if ( alumni.Result != null)
            {
                var customTokenAsync = CreateCustomToken(uid);
                return customTokenAsync;
            }
            return "";
        }


        private async Task<Alumnus> LoadAlumniByUid(string uid)
        {
            return await _alumniRepository.GetFirstOrDefaultAsync(alumnus => alumnus.Uid == uid);
        }

        private string CreateCustomToken(string uid)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
            Console.WriteLine(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim("uid", uid),
                    new Claim(ClaimTypes.Role, RolesConstants.ALUMNI)
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