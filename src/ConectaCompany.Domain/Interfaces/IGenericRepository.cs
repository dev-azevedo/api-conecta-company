using ConectaCompany.Domain.Models;

namespace ConectaCompany.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<(List<T>, int)> GetAllAsync(int skip, int take);
    Task<T?> GetByIdAsync(long id);
    Task<T> CreateAsync(T item);
    T UpdateAsync(T item);
    bool DeleteAsync(T item);
}