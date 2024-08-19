using AngularNetApi.Services.Auth;
using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.ConfirmEmail
{
    public class ConfirmEmailRequestHandler
        : IRequestHandler<ConfirmEmailRequest, ConfirmEmailResponse>
    {
        private readonly IAuthService _authSvc;

        public ConfirmEmailRequestHandler(IAuthService authService)
        {
            _authSvc = authService;
        }

        public async Task<ConfirmEmailResponse> Handle(
            ConfirmEmailRequest request,
            CancellationToken cancellationToken
        )
        {
            return await _authSvc.ConfirmEmailAsync(request);
        }
    }
}
