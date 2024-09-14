using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region store methods
        public async Task<ICollection<StoreFullData>> GetAllStoreAsync()
        {
            return await _db
                .Stores.Where(r => r.IsActive == true)
                .Select(s => new StoreFullData
                {
                    StoreId = s.StoreId,
                    ApplicationUserId = s.ApplicationUserId,
                    StoreName = s.StoreName,
                    Address = s.Address,
                    City = s.City,
                    CAP = s.CAP,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    PhoneNumber = s.PhoneNumber,
                    IsActive = s.IsActive,
                    StoreTag = s.StoreTag,
                    CoverImg = s.CoverImg,
                    LogoImg = s.LogoImg,
                    Description = s.Description,
                    Products = null,
                    WorkingDays = s
                        .JoinStoreWeekDay.Select(jsw => new WorkingDaysFullData
                        {
                            StoreId = jsw.StoreId,
                            WeekDayCode = jsw.WeekDay.WeekDayCode,
                            WeekDayName = jsw.WeekDay.WeekDayName,
                            Order = jsw.WeekDay.Order,
                            WorkingHours = jsw
                                .WorkingHour.Select(jsh => new WorkingHoursData
                                {
                                    ShiftOrder = jsh.ShiftOrder,
                                    OpeningTime = jsh.OpeningTime,
                                    ClosingTime = jsh.ClosingTime,
                                })
                                .ToList()
                        })
                        .ToList(),
                    Categories = s
                        .JoinStoreCategory.Select(jsc => new CategoryData
                        {
                            CategoryId = jsc.Category.CategoryId,
                            CategoryName = jsc.Category.CategoryName
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<StoreFullData?> GetStoreByIdAsync(int storeId)
        {
            return await _db
                .Stores.Where(r => r.IsActive == true && r.StoreId == storeId)
                .Select(s => new StoreFullData
                {
                    StoreId = s.StoreId,
                    ApplicationUserId = s.ApplicationUserId,
                    StoreName = s.StoreName,
                    Address = s.Address,
                    City = s.City,
                    CAP = s.CAP,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    PhoneNumber = s.PhoneNumber,
                    IsActive = s.IsActive,
                    StoreTag = s.StoreTag,
                    CoverImg = s.CoverImg,
                    LogoImg = s.LogoImg,
                    Description = s.Description,
                    Products = s
                        .Products.Select(p => new ProductFullData
                        {
                            ProductId = p.ProductId,
                            StoreId = p.StoreId,
                            ProductName = p.ProductName,
                            ProductPrice = p.ProductPrice,
                            IsActive = p.IsActive,
                            Description = p.Description,
                            ProductImg = p.ProductImg,
                            ProductType = new ProductTypeData
                            {
                                ProductTypeId = p.ProductType.ProductTypeId,
                                ProductTypeName = p.ProductType.ProductTypeName
                            },
                            Ingredients = p
                                .JoinProductIngredient.Select(jpi => new IngredientData
                                {
                                    IngredientId = jpi.Ingredient.IngredientId,
                                    StoreId = jpi.Ingredient.StoreId,
                                    IngredientName = jpi.Ingredient.IngredientName,
                                    IngredientPrice = jpi.Ingredient.IngredientPrice,
                                })
                                .ToList()
                        })
                        .ToList(),
                    WorkingDays = s
                        .JoinStoreWeekDay.Select(jsw => new WorkingDaysFullData
                        {
                            StoreId = jsw.StoreId,
                            WeekDayCode = jsw.WeekDay.WeekDayCode,
                            WeekDayName = jsw.WeekDay.WeekDayName,
                            Order = jsw.WeekDay.Order,
                            WorkingHours = jsw
                                .WorkingHour.Select(jsh => new WorkingHoursData
                                {
                                    ShiftOrder = jsh.ShiftOrder,
                                    OpeningTime = jsh.OpeningTime,
                                    ClosingTime = jsh.ClosingTime,
                                })
                                .ToList()
                        })
                        .ToList(),
                    Categories = s
                        .JoinStoreCategory.Select(jsc => new CategoryData
                        {
                            CategoryId = jsc.Category.CategoryId,
                            CategoryName = jsc.Category.CategoryName
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StoreFullData> CreateStoreAsync(Store store)
        {
            await _db.Stores.AddAsync(store);
            await _db.SaveChangesAsync();

            return await GetStoreByIdAsync(store.StoreId);
        }

        public async Task<StoreFullData> UpdateStoreAsync(Store store)
        {
            _db.Stores.Update(store);
            await _db.SaveChangesAsync();

            return await GetStoreByIdAsync(store.StoreId);
        }
        #endregion

        public async Task<ICollection<CategoryData>> GetAllCategories()
        {
            return await _db
                .Categories.Select(c => new CategoryData
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }
    }
}
