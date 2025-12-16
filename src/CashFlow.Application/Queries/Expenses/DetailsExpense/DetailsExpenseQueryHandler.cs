using AutoMapper;
using CashFlow.Application.Common;
using CashFlow.Application.DTO.ViewModel.Expense;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.Queries.Expenses.DetailsExpense;

public class DetailsExpenseQueryHandler : IRequestHandler<DetailsExpenseQuery, ResultViewModel<DetailsExpenseDto>>
{
    private readonly ILogger<DetailsExpenseQueryHandler> _logger;
    private readonly IReadOnlyRepository<Expense> _readExpenseRepository;
    private readonly IMapper _mapper;

    public DetailsExpenseQueryHandler(ILogger<DetailsExpenseQueryHandler> logger, IReadOnlyRepository<Expense> readExpenseRepository, IMapper mapper)
    {
        _logger = logger;
        _readExpenseRepository = readExpenseRepository;
        _mapper = mapper;
    }

    public async Task<ResultViewModel<DetailsExpenseDto>> Handle(DetailsExpenseQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando busca com Expense com id: {@Id}", request.Id);
        var entity = await _readExpenseRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            _logger.LogDebug("Erro tratado {@Erro}", request.Notifications);
            request.AddNotificationNotFound();
            return ResultViewModel<DetailsExpenseDto>.Failure(Error.NotFound(
                "", request.Notifications));
        }

        var result = _mapper.Map<DetailsExpenseDto>(entity);

        return ResultViewModel<DetailsExpenseDto>.Success(result);
    }
}
