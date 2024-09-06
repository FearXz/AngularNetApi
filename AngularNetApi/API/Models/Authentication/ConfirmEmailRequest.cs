using AngularNetApi.Application.MediatR.Authentication.ConfirmEmail;
using MediatR;

namespace AngularNetApi.API.Models.Authentication
{
    public class ConfirmEmailRequest
    {
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
