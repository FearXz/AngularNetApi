using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.RefreshToken
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
