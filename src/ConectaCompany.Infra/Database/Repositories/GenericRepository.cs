using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ConectaCompany.Infra.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> DbSet;

    protected GenericRepository(AppDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }
    
    public virtual async Task<(List<T>, int)> GetAllAsync(int skip, int take)
    {
        var query = DbSet.AsNoTracking();
        
        var totalItems = await query.CountAsync();
        var setSkip = (skip - 1) * take;
        var items = await query.Skip(setSkip).Take(take).ToListAsync();
        
        return (items, totalItems);
    }

    public virtual async Task<T?> GetByIdAsync(long id)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public virtual async Task<T> CreateAsync(T item)
    {
        await DbSet.AddAsync(item);
        return item;
    }

    public virtual T UpdateAsync(T item)
    {
        DbSet.Update(item);
        return item;
    }

    public virtual bool DeleteAsync(T item)
    {
        DbSet.Remove(item);
        return true;
    }
}