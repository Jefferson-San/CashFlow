using AutoMapper;
using CashFlow.Application.Common;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
namespace CashFlow.Application.Commands.Expenses.Create;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ResultViewModel<Guid>>
{
    private readonly IWriteOnlyRepository<Expense> _writeExpenseRepository;
    private readonly ILogger<CreateExpenseCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateExpenseCommandHandler(
        IWriteOnlyRepository<Expense> writeRepository, 
        ILogger<CreateExpenseCommandHandler> logger, 
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _writeExpenseRepository = writeRepository;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando com request {@Request}", request);
        request.Validate();

        if (!request.IsValid)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            return ResultViewModel<Guid>.Failure(
                 Error.Validation("Ocorreram erros de validação", request.Notifications));
        }

        var entity = _mapper.Map<Expense>(request);
        _logger.LogDebug("Query para adição no banco criada, com a despesa: {@Entity}", entity);
        _writeExpenseRepository.Add(entity);

        _logger.LogDebug("Salvando alterações no banco");
        await _unitOfWork.CommitAsync();

        return ResultViewModel<Guid>.Success(entity.Id);
    }

}
