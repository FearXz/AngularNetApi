using AngularNetApi.API.Models.AccountManagement;
using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Core.Entities;
using AutoMapper;

namespace AngularNetApi.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User Profile
            CreateMap<CreateUserRequest, UserProfile>();
            CreateMap<CreateUserRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            #endregion

            #region Store Profile
            CreateMap<CreateStoreRequest, Store>();
            CreateMap<UpdateStoreRequest, Store>();
            CreateMap<StoreFullData, Store>();
            CreateMap<StoreData, Store>();
            #endregion
        }
    }
}
