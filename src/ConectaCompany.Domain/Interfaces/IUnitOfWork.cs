namespace ConectaCompany.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}