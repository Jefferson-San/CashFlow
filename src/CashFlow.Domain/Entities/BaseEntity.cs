namespace CashFlow.Domain.Entities;

public abstract class BaseEntity
{

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDelete {  get; set; }
    public void SetDelete() => IsDelete = true;
    
    protected BaseEntity()
    {
        Id = Guid.NewGuid() ;
        CreatedAt = DateTime.UtcNow;
        IsDelete = false;
    }
}
