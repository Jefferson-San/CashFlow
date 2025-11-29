using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
