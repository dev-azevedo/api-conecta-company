using ConectaCompany.Api.Setup;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Utils;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuration Project
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddDependencyInjectionConfig(builder.Configuration);
builder.Services.AddJwtConfig(builder.Configuration);
builder.Services.AddSmtpConfig(builder.Configuration);
builder.Services.AddIdentityConfig();
builder.Services.AddMapperConfig();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await DbInitializer.SeedRoles(roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
