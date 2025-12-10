using CashFlow.Application.Common;
using CashFlow.Application.DTO.ViewModel.Expense;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CashFlow.Application.Queries.DetailsExpense;
public class DetailsExpenseQuery : Notifiable<Notification>, IRequest<ResultViewModel<DetailsExpenseDto>>
{
    public DetailsExpenseQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id {  get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<DetailsExpenseQuery>()
            .Requires()
            .IsNotEmpty(Id, "Id")
            );
    }

    public void AddNotificationNotFound()
    {
        AddNotification("Error", "Expense not found");
    }
}
