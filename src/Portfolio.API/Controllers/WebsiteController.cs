using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Extensions;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebsiteController : ControllerBase
{
    private readonly IWebsiteService _websiteService;

    public WebsiteController(IWebsiteService websiteService)
    {
        _websiteService = websiteService;
    }

    [HttpGet]
    public async Task<ActionResult<WebsiteConfigResponseDto>> Get(CancellationToken token)
    {
        var result = await _websiteService.GetWebsiteInfoAsync(token);
        return this.ReturnActionResult(result);
    }

    [HttpPut("changeConfig")]
    public async Task<ActionResult<WebsiteConfigResponseDto>> ChangeConfig(
        [FromBody] WebsiteConfigUpdateRequestDto websiteConfigUpdate, 
        CancellationToken token)
    {
        var result = await _websiteService.UpdateWebsiteConfig(websiteConfigUpdate, token);
        return this.ReturnActionResult(result);
    }

    [HttpPut("changeName")]
    public async Task<ActionResult> ChangeName(
        [FromBody] ChangeNameRequestDto changeNameRequest, 
        CancellationToken token)
    {
        var result = await _websiteService.ChangeNameAsync(changeNameRequest, token);
        return this.ReturnActionResult(result);
    }

    [HttpPut("changePassword")]
    public async Task<ActionResult> ChangePassword(
        [FromBody] ChangePasswordRequestDto changePasswordRequest, 
        CancellationToken token)
    {
        var result = await _websiteService.ChangePasswordAsync(changePasswordRequest, token);
        return this.ReturnActionResult(result);
    }
}
