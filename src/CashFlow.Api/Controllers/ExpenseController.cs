using CashFlow.Application.Commands.Expenses.Create;
using CashFlow.Application.Commands.Expenses.Delete;
using CashFlow.Application.Commands.Expenses.Update;
using CashFlow.Application.Queries.Expenses.DetailsExpense;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand request)
    {
        var result = await _mediator.Send(request);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(Guid id)
    {
        var result = await _mediator.Send(new DetailsExpenseQuery(id));
        if(!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExpense([FromBody] UpdateExpenseCommand request)
    {
        var result = await _mediator.Send(request);
        if(!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var result = await _mediator.Send(new DeleteExpenseCommand(id));
        if(!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }
}
