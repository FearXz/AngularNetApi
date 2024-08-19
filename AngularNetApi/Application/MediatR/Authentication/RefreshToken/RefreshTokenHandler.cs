using AngularNetApi.Services.Auth;
using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly IAuthService _authSvc;

        public RefreshTokenHandler(IAuthService authService)
        {
            _authSvc = authService;
        }

        public Task<RefreshTokenResponse> Handle(
            RefreshTokenRequest request,
            CancellationToken cancellationToken
        )
        {
            return _authSvc.RefreshToken(request);
        }
    }
}
