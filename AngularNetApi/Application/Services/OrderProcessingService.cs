using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Application.Interfaces;
using AngularNetApi.Core.Entities;
using AngularNetApi.Infrastructure.Interfaces;

namespace AngularNetApi.Application.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IStoreRepository _storeRepository;

        public OrderProcessingService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<ICollection<StoreFullData>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllStoreAsync();
        }
    }
}
