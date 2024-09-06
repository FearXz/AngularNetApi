﻿using AngularNetApi.API.Models;
using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Infrastructure.Interfaces
{
    public interface IStoreRepository
    {
        // getallstore

        Task<ICollection<StoreFullData>> GetAllStoreAsync();
        Task<ICollection<CategoryData>> GetAllCategories();
    }
}
