using AngularNetApi.API.Models;
using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.Register
{
    public class CreateUserRequest : IRequest<UserData>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfimPassword { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string CAP { get; set; }

        public string PhoneNumber { get; set; }
    }
}
