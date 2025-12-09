namespace CashFlow.Domain.Repository;
public interface IReadOnlyRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();

}
