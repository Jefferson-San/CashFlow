using CashFlow.Application.Common;
using CashFlow.Domain.Entities;
using CashFlow.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
namespace CashFlow.Application.Commands.CreateExpense;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ResultViewModel<string>>
{
    private readonly IWriteOnlyRepository<Expense> _writeRepository;
    private readonly ILogger<CreateExpenseCommandHandler> _logger;

    public CreateExpenseCommandHandler(IWriteOnlyRepository<Expense> writeRepository, ILogger<CreateExpenseCommandHandler> logger)
    {
        _writeRepository = writeRepository;
        _logger = logger;
    }

    public async Task<ResultViewModel<string>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando com request {@Request}", request);
        request.Validate();

        if (!request.IsValid)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            return ResultViewModel<string>.Failure(
                 Error.Validation("Ocorreram erros de validação", request.Notifications));
        }



        return ResultViewModel<string>.Success("Ola");
    }

}
