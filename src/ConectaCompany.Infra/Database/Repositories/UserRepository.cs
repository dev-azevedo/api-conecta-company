using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConectaCompany.Infra.Database.Repositories;

public class UserRepository(UserManager<User> userManager) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;


    public async Task<User?> GetByIdAsync(long id)
    {
        return await _userManager.Users
            .Include(u => u.Employee)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userManager.Users
            .Include(u => u.Employee)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> CreateAsync(User item, string password)
    {
        var result = await _userManager.CreateAsync(item, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao criar usuário: {errors}");
        }
        
        return item;
    }

    public async Task<User> UpdateAsync(User item)
    {
        var result = await _userManager.UpdateAsync(item);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao atualizar usuário: {errors}");
        }
        
        return item;
    }

    public async Task<User> ChangePasswordAsync(User item, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(item, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao alterar senha do usuário: {errors}");
        }
        
        return item;
    }

    public async Task<User> ResetPasswordAsync(User item, string newPassword)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(item);
        var result = await _userManager.ResetPasswordAsync(item, token, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao redefinir senha do usuário: {errors}");
        }
        
        return item;
    }

    public async Task<bool> DeleteAsync(User item)
    {
        item.Employee.Deactivate();
        item.LockoutEnabled = true;
        item.LockoutEnd = DateTimeOffset.MaxValue;
        var result = await _userManager.UpdateAsync(item);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao excluir usuário: {errors}");
        }
        
        return true;
    }
}