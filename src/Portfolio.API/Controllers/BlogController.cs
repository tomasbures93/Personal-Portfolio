using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new List<string> { "Blog post 1", "Blog post 2", "Blog post 3" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        return Ok($"Blog post {id}");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        return Ok($"Created blog post: {value}");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] string value)
    {
        return Ok($"Updated blog post {id} with value: {value}");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok($"Deleted blog post {id}");
    }
}
