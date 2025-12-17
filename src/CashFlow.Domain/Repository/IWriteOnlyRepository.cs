namespace CashFlow.Domain.Repository;
public interface IWriteOnlyRepository<in T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void DeleteById(T entity);
}
