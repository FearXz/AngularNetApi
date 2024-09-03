using AngularNetApi.API.Models;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Infrastructure.Interfaces
{
    public interface IOrderOpsRepository
    {
        // getallstore

        Task<ICollection<Store>> GetAllStore();
    }
}
