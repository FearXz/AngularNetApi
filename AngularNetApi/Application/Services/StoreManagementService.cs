using System.Runtime.CompilerServices;
using System.Security.Claims;
using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Application.Interfaces;
using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Exceptions;
using AngularNetApi.Core.ValueObjects;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Services.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AngularNetApi.Application.Services
{
    public class StoreManagementService : IStoreManagementService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly IStoreRepository _storeRepository;
        private readonly IAccountService _accountSvc;
        private readonly UserManager<ApplicationUser> _userManager;

        public StoreManagementService(
            IStoreRepository storeRepository,
            IMapper mapper,
            IHttpContextAccessor http,
            UserManager<ApplicationUser> userManager
        )
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _http = http;
            _userManager = userManager;
        }

        #region store methods
        public async Task<ICollection<StoreFullData>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllStoreAsync();
        }

        public async Task<StoreFullData> GetStoreByIdAsync(int storeId)
        {
            return await _storeRepository.GetStoreByIdAsync(storeId);
        }

        public async Task<StoreFullData> CreateStoreAsync(CreateStoreRequest store, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var role = await _userManager.GetRolesAsync(user);
            if (!role.Contains(Roles.COMPANY))
            {
                throw new ForbiddenException("User is not a COMPANY");
            }

            var newStore = _mapper.Map<Store>(store);
            newStore.ApplicationUserId = userId;

            return await _storeRepository.CreateStoreAsync(newStore);
        }

        public async Task<StoreFullData> UpdateStoreAsync(UpdateStoreRequest store, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var role = await _userManager.GetRolesAsync(user);
            if (!role.Contains(Roles.COMPANY))
            {
                throw new ForbiddenException("User is not a COMPANY");
            }

            var storeData = await _storeRepository.GetStoreByIdAsync(store.StoreId);
            if (storeData == null)
            {
                throw new NotFoundException("Store not found");
            }

            if (storeData.ApplicationUserId != userId)
            {
                throw new ForbiddenException("User is not the owner of the store");
            }

            var updatedStore = _mapper.Map<Store>(storeData);
            _mapper.Map(store, updatedStore);
            updatedStore.ApplicationUserId = userId;

            return await _storeRepository.UpdateStoreAsync(updatedStore);
        }
        #endregion

        #region category methods
        public Task<ICollection<CategoryData>> GetAllCategories()
        {
            return _storeRepository.GetAllCategories();
        }
        #endregion
    }
}
