using System.Threading.Tasks;

namespace UniAlumni.Business.Services.Interface
{
    public interface IAuthenticationSvc
    {
        string Authenticate(string accessToken);
    }
}