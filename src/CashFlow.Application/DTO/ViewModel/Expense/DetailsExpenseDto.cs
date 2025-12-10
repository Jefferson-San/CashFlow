using CashFlow.Domain.Enums;

namespace CashFlow.Application.DTO.ViewModel.Expense;

public class DetailsExpenseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
