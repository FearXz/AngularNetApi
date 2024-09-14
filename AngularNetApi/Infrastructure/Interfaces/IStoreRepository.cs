using AngularNetApi.API.Models;
using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Infrastructure.Interfaces
{
    public interface IStoreRepository
    {
        #region store methods

        Task<ICollection<StoreFullData>> GetAllStoreAsync();
        Task<StoreFullData> GetStoreByIdAsync(int storeId);
        Task<StoreFullData> CreateStoreAsync(Store store);
        Task<StoreFullData> UpdateStoreAsync(Store store);

        #endregion

        #region category methods
        Task<ICollection<CategoryData>> GetAllCategories();

        #endregion
    }
}
