using AutoMapper;
using CashFlow.Application.Common;
using CashFlow.Application.DTO.ViewModel.Expense;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.Commands.Expenses.Update;

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ResultViewModel<Guid>>
{
    private readonly IReadOnlyRepository<Expense> _readExpenseRepository;
    private readonly IWriteOnlyRepository<Expense> _writeExpenseRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateExpenseCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateExpenseCommandHandler(
        IReadOnlyRepository<Expense> readExpenseRepository,
        IWriteOnlyRepository<Expense> writeExpenseRepository,
        IMapper mapper,
        ILogger<UpdateExpenseCommandHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _readExpenseRepository = readExpenseRepository;
        _writeExpenseRepository = writeExpenseRepository;
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResultViewModel<Guid>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando com request: {@Request}", request);
        request.Validate();

        if (!request.IsValid)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            return ResultViewModel<Guid>.Failure(
                Error.Validation("Ocorreram erros de validação", request.Notifications));
        }

        var result = await _readExpenseRepository.GetByIdAsync(request.Id);

        if (result is null)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            request.AddNotificationNotFound();
            return ResultViewModel<Guid>.Failure(Error.NotFound(
                "", request.Notifications));
        }

        Expense entity = _mapper.Map<Expense>(result);

        _writeExpenseRepository.UpdateById(entity);

        _unitOfWork.Commit();

        return ResultViewModel<Guid>.Success(entity.Id);
    }
}
