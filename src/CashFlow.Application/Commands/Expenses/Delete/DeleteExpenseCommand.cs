using CashFlow.Application.Common;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System.Diagnostics.Contracts;

namespace CashFlow.Application.Commands.Expenses.Delete;
public class DeleteExpenseCommand : Notifiable<Notification>, IRequest<ResultViewModel<Guid>>
{
    public Guid Id { get; set; }

    public DeleteExpenseCommand(Guid id)
    {
        Id = id;
    }

    public void Validation()
    {
        AddNotifications(
            new Contract<DeleteExpenseCommand>()
            .Requires()
            .IsNotEmpty(Id, "Id")
            );
    }

    public void AddNotificationNotFound() => AddNotification("Error", "Expense not found");
}
