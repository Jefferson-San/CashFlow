
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Repository;

public class ReadOnlyRepositoryAsync<T> : IReadOnlyRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    public ReadOnlyRepositoryAsync(DbContext dbContext) { _dbContext = dbContext; }

    public async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);

        if(entity is not null)
            _dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }
}
