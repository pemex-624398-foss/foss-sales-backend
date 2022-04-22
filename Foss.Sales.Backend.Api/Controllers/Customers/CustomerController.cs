using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pemex.Foss.Sales.Backend.Api.Core.Model;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Queries;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Pemex.Foss.Sales.Backend.Api.Controllers.Customers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _logger = Log.ForContext<CustomerController>();
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{customerId:int}")]
    [ProducesResponseType(
        typeof(CustomerResult), 
        StatusCodes.Status200OK,
        MediaTypeNames.Application.Json
    )]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int customerId)
    {
        var argument = new GetCustomerByIdQuery.Argument(customerId);
            
        _logger.Debug("Get customer by id query argument {@Argument}", argument);
        var result = await _mediator.Send(argument);
        
        _logger.Debug("Get customer by id query result {@Result}", result);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpGet]
    [ProducesResponseType(
        typeof(IEnumerable<CustomerResult>), 
        StatusCodes.Status200OK,
        MediaTypeNames.Application.Json
    )]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByFilter([FromQuery] GetCustomersByFilterQuery.Argument argument)
    {
        _logger.Debug("Get customer by name query argument {@Argument}", argument);
        var result = await _mediator.Send(argument);
        
        _logger.Debug("Get customer by name query result count {@ResultCount}", result.Count());
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(CreateCustomerCommand.Result), 
        StatusCodes.Status201Created,
        MediaTypeNames.Application.Json
    )]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand.Argument argument)
    {
        _logger.Debug("Create customer command argument {@Argument}", argument);
        var result = await _mediator.Send(argument);
        
        _logger.Debug("Create customer command result {@Result}", result);
        return Created(new Uri($"{Request.Path}/{result.CustomerId}", UriKind.Relative), result);
    }

    [HttpPut]
    [Route("{customerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Rename(int customerId, [FromBody] RenameCustomerCommand.Argument argument)
    {
        if (argument.CustomerId != 0 && argument.CustomerId != customerId)
            return BadRequest();

        argument.CustomerId = customerId;

        try
        {
            _logger.Debug("Rename customer command argument {@Argument}", argument);
            await _mediator.Send(argument);

            _logger.Debug("Rename customer command executed");
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (MultipleEntityAffectedException)
        {
            return Conflict();
        }
    }

    [HttpPut]
    [Route("{customerId:int}/email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEmail(int customerId, [FromBody] UpdateCustomerEmailCommand.Argument argument)
    {
        if (argument.CustomerId != 0 && argument.CustomerId != customerId)
            return BadRequest();

        argument.CustomerId = customerId;

        try
        {
            _logger.Debug("Update customer email command argument {@Argument}", argument);
            await _mediator.Send(argument);
        
            _logger.Debug("Update customer email command executed");
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (MultipleEntityAffectedException)
        {
            return Conflict();
        }
    }

    [HttpDelete]
    [Route("{customerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteById(int customerId)
    {
        try
        {
            var argument = new DeleteCustomerCommand.Argument(customerId);
            _logger.Debug("Delete customer command argument {@Argument}", argument);
            await _mediator.Send(argument);
        
            _logger.Debug("Delete customer command executed");
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (MultipleEntityAffectedException)
        {
            return Conflict();
        }
    }
}