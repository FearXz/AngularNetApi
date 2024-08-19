namespace AngularNetApi.Application.MediatR.Authentication.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
