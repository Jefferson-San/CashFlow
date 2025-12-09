using CashFlow.Domain.Repository;
using CashFlow.Infrastructure.Context;

namespace CashFlow.Infrastructure.Repository;

public class WriteOnlyRepositoryAsync<T> : IWriteOnlyRepository<T> where T : class
{
    private readonly CashFlowDbContext _dbContext;

    public WriteOnlyRepositoryAsync(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(T entity) =>  _dbContext.Set<T>().Add(entity);

    public void DeleteById(T entity) => _dbContext.Remove(entity);

    public async Task UpdateById(T entity) => _dbContext.Update(entity);
    
}
