using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConectaCompany.Infra.Database.Repositories;

public class UnitOfWork(AppDbContext context, UserManager<User> userManager) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IDbContextTransaction? _transaction;

    // Repositórios
    private IUserRepository? _userRepository;

    public IUserRepository Users => _userRepository ??= new UserRepository(userManager);

    public async Task BeginTransactionAsync()
    {
        if (_transaction is null)
            _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction is not null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        } 
        catch
        {
            await RollbackAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}