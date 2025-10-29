using ConectaCompany.Domain.Models;

namespace ConectaCompany.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User user, string password);
    Task<User> UpdateAsync(User user);
    Task AddRoleAsync(User user, string role);
    Task<User> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<User> ResetPasswordAsync(User user, string newPassword);
    Task<string> GenerateTokenConfirmEmailAsync(User user);
    Task<bool> ConfirmEmailAsync(User user, string token);
    Task<bool> DeleteAsync(User user);
}