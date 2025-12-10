using CashFlow.Domain.Repository;
using CashFlow.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Repository;

public class ReadOnlyRepositoryAsync<T> : IReadOnlyRepository<T> where T : class
{
    private readonly CashFlowDbContext _dbContext;

    public ReadOnlyRepositoryAsync(CashFlowDbContext dbContext) { _dbContext = dbContext; }

    public async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);

        if(entity is not null)
            _dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }
}
