using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Entities;
using Portfolio.Application.Abstraction.Validator;

namespace Portfolio.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IWebsiteRepository _websiteRepository;
    private readonly PasswordHasher<WebsiteConfig> _passwordHasher = new();
    private readonly IValidate<LoginRequestDto> _validate;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IWebsiteRepository websiteRepository, IValidate<LoginRequestDto> validate, ILogger<AuthService> logger)
    {
        _websiteRepository = websiteRepository;
        _validate = validate;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken token)
    {
        _logger.LogInformation("Login attempt with username: {UserName}", loginRequestDto.login);

        var validationResult = _validate.Validate(loginRequestDto);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Login validation failed for username: {UserName}. Errors: {Errors}", loginRequestDto.login, string.Join(", ", validationResult.Errors));
            return Result<LoginResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);
        }

        var config = await _websiteRepository.GetWebsiteInfoAsync(token);
        if (config == null)
        {
            _logger.LogError("Critical error - Website configuration not found during login attempt");
            Result<LoginResponseDto>.Failure(ResultStatus.Error, "Critical Error - Website configuration not found!!");
        }

        var result = _passwordHasher.VerifyHashedPassword(config!, config!.PasswordHash, loginRequestDto.password);

        if(result == PasswordVerificationResult.Failed || config.UserName != loginRequestDto.login)
        {
            _logger.LogWarning("Failed login attempt for username: {UserName}. Reason: Invalid credentials", loginRequestDto.login);
            return Result<LoginResponseDto>.Failure(ResultStatus.Unauthorized, "Invalid login or password");
        }

        _logger.LogInformation("Successful login for username: {UserName}", config.UserName);
        return Result<LoginResponseDto>.Ok(new LoginResponseDto(true, config.UserName, config.Email));
    }
}
