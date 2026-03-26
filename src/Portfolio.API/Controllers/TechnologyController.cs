using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Extensions;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologyController : ControllerBase
{
    private readonly ITechnologyService _technologyService;

    public TechnologyController(ITechnologyService technologyService)
    {
        _technologyService = technologyService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TechnologyResponseDto>>> Get(CancellationToken token)
    {
        var result = await _technologyService.GetTechnologiesAsync(token);
        return this.ReturnActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TechnologyResponseDto>> GetSingle(int id, CancellationToken token)
    {
        var result = await _technologyService.GetTechnologyAsync(id, token);
        return this.ReturnActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<TechnologyResponseDto>> Post([FromBody] TechnologyRequestDto technologyCreate, CancellationToken token)
    {
        var result = await _technologyService.CreateTechnologyAsync(technologyCreate, token);
        return this.ReturnActionResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<TechnologyResponseDto>> Put([FromBody] TechnologyUpdateRequestDto technologyUpdate, CancellationToken token)
    {
        var result = await _technologyService.UpdateTechnologyAsync(technologyUpdate, token);
        return this.ReturnActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken token)
    {
        var result = await _technologyService.DeleteTechnologyAsync(id, token);
        return this.ReturnActionResult(result);
    }
}
