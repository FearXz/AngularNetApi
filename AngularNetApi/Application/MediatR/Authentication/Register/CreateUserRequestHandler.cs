using AngularNetApi.API.Models;
using AngularNetApi.Services.User;
using MediatR;

namespace AngularNetApi.Application.MediatR.Authentication.Register
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, UserData>
    {
        private readonly IAccountService _accountSvc;

        public CreateUserRequestHandler(IAccountService userService)
        {
            _accountSvc = userService;
        }

        public Task<UserData> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            return _accountSvc.CreateAsync(request);
        }
    }
}
