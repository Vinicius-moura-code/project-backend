using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ModelToDtoMappingUser : Profile
    {

        public ModelToDtoMappingUser()
        {
            UserDtoMap();
        }

        public void UserDtoMap()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.MapFrom(c => c.Username))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(c => c.Role));
        }

    }
}
