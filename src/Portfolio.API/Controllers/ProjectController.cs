using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Extensions;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponseDto>>> Get(CancellationToken token)
    {
        var result = await _projectService.GetProjectsAsync(token);
        return this.ReturnActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponseDto>> GetSingle(
        int id, 
        CancellationToken token)
    {
        var result = await _projectService.GetProjectAsync(id, token);
        return this.ReturnActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> Post(
        [FromBody] ProjectRequestDto projectRequestDto, 
        CancellationToken token)
    {
        var result = await _projectService.CreateProjectAsync(projectRequestDto, token);
        return this.ReturnActionResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<ProjectResponseDto>> Put(
        [FromBody] ProjectUpdateRequestDto projectUpdateRequest, 
        CancellationToken token)
    {
        var result = await _projectService.UpdateProjectAsync(projectUpdateRequest, token);
        return this.ReturnActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken token)
    {
        var result = await _projectService.DeleteProjectAsync(id, token);
        return this.ReturnActionResult(result);
    }
}
