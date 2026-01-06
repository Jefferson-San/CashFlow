using AutoMapper;
using CashFlow.Application.Commands.Expenses.Create;
using CashFlow.Application.Commands.Expenses.Update;
using CashFlow.Application.DTO.ViewModel.Expense;
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
        CreateMap<UpdateExpenseCommand, Expense>();
    }

    private void MapToViewModel()
    {
        CreateMap<Expense, DetailsExpenseDto>();
    }
}
