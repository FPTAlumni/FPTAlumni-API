namespace UniAlumni.DataTier.ViewModels.Token
{
    public class TokenResponse
    {
        public TokenResponse(string customToken, int id)
        {
            CustomToken = customToken;
            Id = id;
        }

        public string CustomToken { get; set; }
        public int? Id { get; set; }

        public static TokenResponse BuildTokenResponse(string token, int id)
            => new TokenResponse(token, id);
    }
}