using System.Threading.Tasks;

namespace UniAlumni.Business.Services.Interface
{
    /// <summary>
    /// Interface for service layer of Authentication in Business module.
    /// </summary>
    public interface IAuthenticationSvc
    {
        string Authenticate(string accessToken);
    }
}