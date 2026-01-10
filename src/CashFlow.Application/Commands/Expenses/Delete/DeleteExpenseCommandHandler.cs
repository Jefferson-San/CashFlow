using CashFlow.Application.Common;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.Commands.Expenses.Delete;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, ResultViewModel<Guid>>
{
    private readonly IReadOnlyRepository<Expense> _readExpenseRepository;
    private readonly IWriteOnlyRepository<Expense> _writeExpenseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteExpenseCommandHandler> _logger;

    public DeleteExpenseCommandHandler(
        IReadOnlyRepository<Expense> readExpenseRepository, 
        IWriteOnlyRepository<Expense> writeExpenseRepository, 
        IUnitOfWork unitOfWork, ILogger<DeleteExpenseCommandHandler> logger)
    {
        _readExpenseRepository = readExpenseRepository;
        _writeExpenseRepository = writeExpenseRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ResultViewModel<Guid>> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request with Id: {@Request}", request.Id);
        request.Validation();
        
        if (!request.IsValid)
        {
            _logger.LogDebug("Error handled {@Error}", request.Notifications);
            return ResultViewModel<Guid>.Failure(
                Error.Validation("Validation error", request.Notifications));
        }

        var entity = await _readExpenseRepository.GetByIdAsync(request.Id);
        if (entity is null)
        {
            _logger.LogDebug("Error handled {@Error}", request.Notifications);
            request.AddNotificationNotFound();
            return ResultViewModel<Guid>.Failure(Error.NotFound(
                "Expense not found", request.Notifications));
        }

        entity.SetDelete();

        _logger.LogDebug("Delete query created for database, with expense: {@Entity}", entity);
        _writeExpenseRepository.Update(entity);

        _logger.LogDebug("Saving changes to database");
        await _unitOfWork.CommitAsync();

        return ResultViewModel<Guid>.Success(request.Id);
    }
}
