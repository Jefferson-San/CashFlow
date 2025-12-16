using CashFlow.Application.Commands.Expenses.Create;
using CashFlow.Application.Common;
using CashFlow.Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CashFlow.Application.Commands.Expenses.Update;
public class UpdateExpenseCommand : Notifiable<Notification>, IRequest<ResultViewModel<Guid>>
{
    public Guid Id { get; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
    public PaymentType PaymentType { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<CreateExpenseCommand>()
            .Requires()
            .IsNotEmpty(Id, "Id")
            .IsNotNullOrEmpty(Title, "Title")
            .IsGreaterThan(0, Amount, "Amount")
            .IsTrue(Enum.IsDefined(typeof(PaymentType), PaymentType), "PaymentType", "Invalid value")
            );
    }

    public void AddNotificationNotFound()
    {
        AddNotification("Error", "Expense not found");
    }
}