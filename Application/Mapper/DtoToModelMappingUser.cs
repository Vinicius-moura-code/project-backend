using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class DtoToModelMappingUser : Profile
    {
        public DtoToModelMappingUser()
        {
            UserModelMap();
        }

        public void UserModelMap()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.MapFrom(c => c.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(c => c.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(c => c.Role))
;
        }
    }
}
