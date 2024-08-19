using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
