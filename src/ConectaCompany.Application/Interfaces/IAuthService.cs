using ConectaCompany.Domain.Models;

namespace ConectaCompany.Application.Interfaces;

public interface IAuthService
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> SignInAsync(string email, string password);
    Task<User> SignUpAsync(User item, string password);
}