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
        _logger.LogInformation("Starting with request: {@Request}", request);
        request.Validate();

        if (!request.IsValid)
        {
            _logger.LogDebug("Error handled {@Error}", request.Notifications);
            return ResultViewModel<Guid>.Failure(
                Error.Validation("Validation errors occurred", request.Notifications));
        }

        var result = await _readExpenseRepository.GetByIdAsync(request.Id);

        if (result is null)
        {
            _logger.LogDebug("Error handled {@Error}", request.Notifications);
            request.AddNotificationNotFound();
            return ResultViewModel<Guid>.Failure(Error.NotFound(
                "", request.Notifications));
        }

        Expense entity = _mapper.Map<Expense>(request);

        _logger.LogDebug("Update query created for database, with expense: {@Entity}", entity);
        _writeExpenseRepository.Update(entity);

        _logger.LogDebug("Saving changes to database");
        await _unitOfWork.CommitAsync();

        return ResultViewModel<Guid>.Success(entity.Id);
    }
}
