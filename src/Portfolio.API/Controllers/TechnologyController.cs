using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologyController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new List<string> { "Technology 1", "Technology 2", "Technology 3" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        return Ok($"Technology {id}");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        return Ok($"Technology added: {value}");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] string value)
    {
        return Ok($"Updated technology {id} with value: {value}");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok($"Deleted technology {id}");
    }
}
