using CashFlow.Application.Common;
using CashFlow.Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CashFlow.Application.Commands.Expenses.Create;
public class CreateExpenseCommand : Notifiable<Notification>, IRequest<ResultViewModel<Guid>> 
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
    public PaymentType PaymentType { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<CreateExpenseCommand>()
            .Requires()
            .IsNotNullOrEmpty(Title, "Title")
            .IsGreaterThan(0, Amount, "Amount")
            .IsTrue(Enum.IsDefined(typeof(PaymentType), PaymentType), "PaymentType", "Invalid value")
            );
    }
}
