using ConectaCompany.Api.Setup;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Utils;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Configuration Project Services
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddDependencyInjectionConfig(builder.Configuration);
builder.Services.AddJwtConfig(builder.Configuration);
builder.Services.AddSmtpConfig(builder.Configuration);
builder.Services.AddIdentityConfig();
builder.Services.AddMapperConfig();
builder.Services.AddSwaggerConfig();


var app = builder.Build();

// Configuration Project Application
app.UseIdentityConfig();
app.UseSwaggerConfig(builder.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
