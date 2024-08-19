using AngularNetApi.Services.User;
using MediatR;

namespace AngularNetApi.Application.MediatR.ProfileManagement.User.CreateUser
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, UserData>
    {
        private readonly IUserService _userSvc;

        public CreateUserRequestHandler(IUserService userService)
        {
            _userSvc = userService;
        }

        public Task<UserData> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            return _userSvc.CreateAsync(request);
        }
    }
}
