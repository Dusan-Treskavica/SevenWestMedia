using AutoMapper;
using SevenWestMedia.Common.DTOs;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.Profiles
{
    public class UserDTOProfile : Profile
    {
        public UserDTOProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.First))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Last));
        }
    }
}