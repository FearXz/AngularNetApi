using AngularNetApi.Services.Auth;
using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAuthService _authSvc;

        public LoginRequestHandler(IAuthService authService)
        {
            _authSvc = authService;
        }

        public async Task<LoginResponse> Handle(
            LoginRequest request,
            CancellationToken cancellationToken
        )
        {
            return await _authSvc.Login(request);
        }
    }
}
