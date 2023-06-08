using AutoMapper;
using DTO;
using Model;

namespace MyMapper
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            CreateMap<UserDto,User>();
            CreateMap<User,UserDto>();
            // Add more mappings as needed
        }
    }
}