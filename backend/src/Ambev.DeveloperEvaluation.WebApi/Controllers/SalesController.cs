using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Sales;


namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private static readonly Dictionary<Guid, Sale> _sales = new();

    [HttpPost]
    public IActionResult Create([FromBody] CreateSaleCommand command)
    {
        try
        {
            var handler = new CreateSaleHandler();
            var sale = handler.Handle(command);
            _sales[sale.Id] = sale;

            return CreatedAtAction(
                nameof(GetById),
                new { id = sale.Id },
                sale
            );
        } 
        catch (Exception ex) 
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        if (!_sales.TryGetValue(id, out var sale))
            return NotFound();

        return Ok(sale);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_sales.Values);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id)
    {
        return Ok($"Update sale {id}");
    }

    [HttpDelete("{id}")]
    public IActionResult Cancel(Guid id)
    {
        if (!_sales.TryGetValue(id, out var sale))
            return NotFound();

        try
        {
            sale.Cancel();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
