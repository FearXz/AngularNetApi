using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Entities.Join;
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

        public async Task<ICollection<StoreFullData>> GetStoreByIdAsync(int storeId)
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
                .ToListAsync();
        }

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
