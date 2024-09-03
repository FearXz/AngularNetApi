using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Entities.Join;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Infrastructure.Repositories
{
    public class OrderOpsRepository : IOrderOpsRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderOpsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Store>> GetAllStore()
        {
            var Stores = await _db.Stores.Where(r => r.IsActive == true).ToListAsync();

            return Stores;
        }
    }
}
