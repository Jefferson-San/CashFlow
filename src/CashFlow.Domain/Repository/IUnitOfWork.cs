namespace CashFlow.Domain.Repository;

public interface IUnitOfWork
{
    Task CommitAsync();
}
