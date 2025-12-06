using AutoMapper;
using CashFlow.Application.Commands.CreateExpense;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Mapper;
public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        MapToEntity();
        MapToViewModel();
    }

    private void MapToEntity()
    {
        CreateMap<CreateExpenseCommand, Expense>();
    }

    private void MapToViewModel()
    {

    }
}
