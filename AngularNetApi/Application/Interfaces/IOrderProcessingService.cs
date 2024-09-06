using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Application.Interfaces
{
    public interface IOrderProcessingService
    {
        Task<ICollection<StoreFullData>> GetAllStoresAsync();
        Task<ICollection<StoreFullData>> GetStoreByIdAsync(int storeId);
        Task<ICollection<CategoryData>> GetAllCategories();
    }
}
