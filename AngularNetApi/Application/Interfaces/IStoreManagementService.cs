using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Application.Interfaces
{
    public interface IStoreManagementService
    {
        #region store methods
        Task<ICollection<StoreFullData>> GetAllStoresAsync();
        Task<StoreFullData> GetStoreByIdAsync(int storeId);
        Task<StoreFullData> CreateStoreAsync(CreateStoreRequest store, string userId);
        Task<StoreFullData> UpdateStoreAsync(UpdateStoreRequest store, string userId);
        #endregion

        #region category methods
        Task<ICollection<CategoryData>> GetAllCategories();
        #endregion
    }
}
