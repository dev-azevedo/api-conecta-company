using ConectaCompany.Domain.Models;

namespace ConectaCompany.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User item, string password);
    Task<User> UpdateAsync(User item);
    Task<User> ChangePasswordAsync(User item, string currentPassword, string newPassword);
    Task<User> ResetPasswordAsync(User item, string newPassword);
    Task<bool> DeleteAsync(User item);
}