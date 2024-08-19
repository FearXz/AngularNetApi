namespace AngularNetApi.Application.MediatR.Authentication.RefreshToken
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
