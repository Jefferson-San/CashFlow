namespace CashFlow.Infrastructure.Repository;
public interface IWriteOnlyRepository<T> where T : class
{
    void Add(T entity);
    Task UpdateById(T entity);
    void DeleteById(T entity);
}
