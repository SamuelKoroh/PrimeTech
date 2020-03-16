using AutoMapper;
using PrimeTech.Core.Models;
using PrimeTech.Infrastructure.Resources.Auth;

namespace PrimeTech.Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterResource, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
