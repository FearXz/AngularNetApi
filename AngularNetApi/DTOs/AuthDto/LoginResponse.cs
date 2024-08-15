namespace AngularNetApi.DTOs.AuthDto
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
