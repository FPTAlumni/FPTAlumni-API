namespace UniAlumni.Business.Services.AuthenticationService
{
    /// <summary>
    /// Interface for service layer of Authentication in Business module.
    /// </summary>
    public interface IAuthenticationSvc
    {
        string Authenticate(string accessToken);
    }
}