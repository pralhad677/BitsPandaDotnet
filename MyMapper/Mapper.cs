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
            CreateMap<AdminDto,Admins.Admin>();
            CreateMap<Admins.Admin,AdminDto>();
            // Add more mappings as needed
        }
    }
}