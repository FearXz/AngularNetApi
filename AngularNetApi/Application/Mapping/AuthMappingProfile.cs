using AngularNetApi.API.Models.Profile;
using AngularNetApi.Core.Entities;
using AutoMapper;

namespace AngularNetApi.Application.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<CreateUserRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateUserRequest, UserProfile>();
        }
    }
}
