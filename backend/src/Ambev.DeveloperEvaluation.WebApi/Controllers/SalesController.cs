using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    [HttpPost]
    public IActionResult Create()
    {
        return Ok("Sale created");
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
