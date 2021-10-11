
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Token;

namespace UniAlumni.Business.Services.AuthenticationService
{
    /// <summary>
    /// Interface for service layer of Authentication in Business module.
    /// </summary>
    public interface IAuthenticationSvc
    {
        Task<TokenResponse> Authenticate(string accessToken, int universityId);
    }
}