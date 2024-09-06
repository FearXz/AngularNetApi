using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Application.Interfaces
{
    public interface IOrderProcessingService
    {
        public Task<ICollection<StoreFullData>> GetAllStoresAsync();
        public Task<ICollection<CategoryData>> GetAllCategories();
    }
}
