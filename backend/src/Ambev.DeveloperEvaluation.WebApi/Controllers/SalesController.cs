using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateSaleCommand command)
    {
        try
        {
            var handler = new CreateSaleHandler();
            var sale = handler.Handle(command);

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
        return Ok($"Get sale {id}");
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("List sales");
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id)
    {
        return Ok($"Update sale {id}");
    }

    [HttpDelete("{id}")]
    public IActionResult Cancel(Guid id)
    {
        return NoContent();
    }
}
