using ConectaCompany.Application.Dto;
using ConectaCompany.Application.Dto.Auth;
using ConectaCompany.Application.Response;
using ConectaCompany.Domain.Models;

namespace ConectaCompany.Application.Interfaces;

public interface IAuthService
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<Result<User>> SignInAsync(string email);
    Task<Result<User>> SignUpAsync(UserDto userDto);
    Task<string> GenerateTokenConfirmEmailAsync(long userId);
    Task<bool> ConfirmEmailAsync(long userId, string token);
    Task<User> InviteUserAsync(InviteUserDto inviteUserDto);
    string GenerateTemplateConfimationEmail(string userFullName, string confirmationLink);
}