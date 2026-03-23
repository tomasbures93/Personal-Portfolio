using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new List<string> { "Project 1", "Project 2", "Project 3" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        return Ok($"Project {id}");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        return Ok($"Created project: {value}");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] string value)
    {
        return Ok($"Updated project {id} with value: {value}");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok($"Deleted project {id}");
    }
}
