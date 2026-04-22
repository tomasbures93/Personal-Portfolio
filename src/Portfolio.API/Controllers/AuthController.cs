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
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken token)
    {
        _logger.LogInformation("Login request received for username: {UserName}", loginRequestDto.Login);

        var result = await _authService.LoginAsync(loginRequestDto, token);
        if(result.Errors.Any())
        {
            _logger.LogWarning("Login failed for username: {UserName}. Status: {Status}", loginRequestDto.Login, result.Status);
            return this.ReturnActionResult(result);
        }

        // TODO : Abstract claims creation to a separate service

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, result.Value.UserName),
            new Claim(ClaimTypes.Email, result.Value.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        _logger.LogInformation("User {UserName} successfully signed in. CorrelationId:{TraceId}", result.Value.UserName, HttpContext.TraceIdentifier);

        return this.ReturnActionResult(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout(CancellationToken token)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
        _logger.LogInformation("Logout request received for user: {UserName}", userName);

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        _logger.LogInformation("User {UserName} successfully signed out. CorrelationId:{TraceId}", userName, HttpContext.TraceIdentifier);

        return Ok(new { message = "Logged out successfully" });
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
