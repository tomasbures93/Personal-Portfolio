using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebsiteController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok("Website Info");
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] string value)
    {
        return Ok(value);
    }
}
