using System;
using System.Threading.Tasks;

namespace UniAlumni.Business.Services.AuthenticationService
{
    /// <summary>
    /// Interface for service layer of Authentication in Business module.
    /// </summary>
    public interface IAuthenticationSvc
    {
        Task<String> Authenticate(string accessToken, int universityId
        );
    }
}