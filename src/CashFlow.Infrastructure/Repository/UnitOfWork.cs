using CashFlow.Domain.Repository;
using CashFlow.Infrastructure.Context;

namespace CashFlow.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDbContext _dbContext;

    public UnitOfWork(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}
