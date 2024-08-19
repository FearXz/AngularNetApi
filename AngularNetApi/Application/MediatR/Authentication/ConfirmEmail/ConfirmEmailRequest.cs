using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.ConfirmEmail
{
    public class ConfirmEmailRequest : IRequest<ConfirmEmailResponse>
    {
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
