using AngularNetApi.API.Models;
using MediatR;

namespace AngularNetApi.API.Models.AccountManagement
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string CAP { get; set; }

        public string PhoneNumber { get; set; }
    }
}
