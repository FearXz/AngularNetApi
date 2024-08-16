using AngularNetApi.DTOs.User;
using AngularNetApi.Entities;
using AutoMapper;

namespace AngularNetApi.MappingProfile
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
