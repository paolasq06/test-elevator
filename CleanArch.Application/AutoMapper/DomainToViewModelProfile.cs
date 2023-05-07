using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Rol;
using Domain.Models.User;

namespace CleanArch.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            //User
            CreateMap<User, UserDto>();
            CreateMap<Rol, RolDto>();
            CreateMap<UserRol, UserRolDto>();


        }
    }
}