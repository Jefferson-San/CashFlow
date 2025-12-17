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
        _logger.LogInformation("Iniciando request com Id: {@Request}", request.Id);
        request.Validation();
        
        if (!request.IsValid)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            return ResultViewModel<Guid>.Failure(
                Error.Validation("Erro de validação", request.Notifications));
        }

        var entity = await _readExpenseRepository.GetByIdAsync(request.Id);
        if (entity is null)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            request.AddNotificationNotFound();
            return ResultViewModel<Guid>.Failure(Error.NotFound(
                "Despesa não encontrada", request.Notifications));
        }

        entity.SetDelete();

        _logger.LogDebug("Query para remoção no banco criada, com a despesa: {@Entity}", entity);
        _writeExpenseRepository.Update(entity);

        _logger.LogDebug("Salvando alterações no banco");
        await _unitOfWork.CommitAsync();

        return ResultViewModel<Guid>.Success(request.Id);
    }
}
