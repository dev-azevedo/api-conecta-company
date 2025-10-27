using ConectaCompany.Application.Dto;
using ConectaCompany.Application.Dto.Auth;
using ConectaCompany.Domain.Models;
using Mapster;

namespace ConectaCompany.Application.Mappers;

public static class SetMapper
{
    public static void RegisterMappings()
    {
        // UserDto -> User
        TypeAdapterConfig<UserDto, User>
            .NewConfig()
            .Map(dest => dest.FullName, src => src.Fullname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.UserName, src => src.Email)
            .Ignore(dest => dest.Employee)
            .Ignore(dest => dest.Created);

        // User -> UserDto
        TypeAdapterConfig<User, UserDto>
            .NewConfig()
            .Map(dest => dest.Fullname, src => src.FullName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, _ => string.Empty)
            .Map(dest => dest.ConfirmPassword, _ => string.Empty);
        
        // InviteUserDto -> User
        TypeAdapterConfig<InviteUserDto, User>
            .NewConfig()
            .Map(dest => dest.FullName, src => src.Fullname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.UserName, src => src.Email)
            .Ignore(dest => dest.Employee)
            .Ignore(dest => dest.Created);
    }
}