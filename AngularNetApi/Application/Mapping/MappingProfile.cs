using AngularNetApi.Application.MediatR.Authentication.Register;
using AngularNetApi.Core.Entities;
using AutoMapper;

namespace AngularNetApi.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create a mapping from CreateUserRequest to UserProfile
            CreateMap<CreateUserRequest, UserProfile>();
            // Create a mapping from CreateUserRequest to ApplicationUser
            CreateMap<CreateUserRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
