using AngularNetApi.Conext;
using AngularNetApi.Entities;
using AngularNetApi.Repository;

namespace AngularNetApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _db;

        //private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ApplicationDbContext db)
        {
            _userRepository = userRepository;
            _db = db;
            //_mapper = mapper;
        }

        public Task<UserProfile> CreateAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> GetByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> UpdateAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }
    }
}
