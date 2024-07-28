using AngularNetApi.Services.DbServices;

namespace AngularNetApi.Services
{
    public class DbContext
    {
        public UserService userSvc { get; set; }

        public DbContext(UserService userService)
        {
            userSvc = userService;
        }
    }
}
