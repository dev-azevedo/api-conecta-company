using ConectaCompany.Application.Mappers;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Context;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Api.Setup;

public static class MapperConfig
{
    public static async void AddMapperConfig(this IServiceCollection services)
    {
        SetMapper.RegisterMappings();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}