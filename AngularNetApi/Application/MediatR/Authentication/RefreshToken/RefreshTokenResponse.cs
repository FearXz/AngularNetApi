namespace AngularNetApi.Application.MediatR.Authentication.RefreshToken
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
