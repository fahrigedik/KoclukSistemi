using AutoMapper;
using MS.AuthServer.Core.Configuration;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Entities;

namespace MS.AuthServer.Service.AutoMapper;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<CreateUserDto, AppUser>().ReverseMap();
        CreateMap<AppUser, AppUserDto>().ReverseMap();
    }
}

