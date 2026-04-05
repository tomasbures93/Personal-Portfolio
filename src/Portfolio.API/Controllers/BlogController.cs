using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Extensions;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BlogPostResponseDto>>> Get(CancellationToken token)
    {
        var result = await _blogService.GetBlogsAsync(token);
        return this.ReturnActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostResponseDto>> GetSingle(
        int id, 
        CancellationToken token)
    {
        var result = await _blogService.GetBlogAsync(id, token);
        return this.ReturnActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<BlogPostResponseDto>> Post(
        [FromBody] BlogRequestDto blogRequestDto, 
        CancellationToken token)
    {
        var user = HttpContext.User.Identity?.Name;
        var result = await _blogService.CreateBlogAsync(blogRequestDto, user!, token);
        return this.ReturnActionResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<BlogPostResponseDto>> Put(
        [FromBody] BlogUpdateRequestDto blogUpdateRequest,
        CancellationToken token)
    {
        var result = await _blogService.UpdateBlogAsync(blogUpdateRequest, token);
        return this.ReturnActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken token)
    {
        var result = await _blogService.DeleteBlogAsync(id, token);
        return this.ReturnActionResult(result);
    }
}
