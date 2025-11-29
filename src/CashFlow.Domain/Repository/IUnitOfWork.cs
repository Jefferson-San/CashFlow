namespace CashFlow.Domain.Repository;

public interface IUnitOfWork
{
    void Commit();
}
