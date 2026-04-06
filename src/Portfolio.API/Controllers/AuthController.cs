using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Extensions;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.DTO.Request;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken token)
    {
        // TODO: ADD Logging Success/Failure Login Attempt
        var result = await _authService.LoginAsync(loginRequestDto, token);
        if(result.Errors.Any())
            return this.ReturnActionResult(result);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, result.Value.userName),
            new Claim(ClaimTypes.Email, result.Value.email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return this.ReturnActionResult(result);
    }

    [HttpGet("csrf-token")]
    public async Task<ActionResult> GetCsrfToken([FromServices] IAntiforgery antiforgery)
    {
        var token = antiforgery.GetAndStoreTokens(HttpContext);

        return Ok(new
        {
            csfToken = token.RequestToken
        });
    }
}
