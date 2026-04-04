using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;
using Microsoft.AspNetCore.Identity;
using Portfolio.Domain.Entities;
using Portfolio.Application.Abstraction.Validator;

namespace Portfolio.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IWebsiteRepository _websiteRepository;
    private readonly PasswordHasher<WebsiteConfig> _passwordHasher = new();
    private readonly IValidate<LoginRequestDto> _validate;

    public AuthService(IWebsiteRepository websiteRepository, IValidate<LoginRequestDto> validate)
    {
        _websiteRepository = websiteRepository;
        _validate = validate;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken token)
    {
        var validationResult = _validate.Validate(loginRequestDto);
        if (validationResult.Errors.Any())
            return Result<LoginResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var config = await _websiteRepository.GetWebsiteInfoAsync(token);
        if (config == null)
            Result<LoginResponseDto>.Failure(ResultStatus.Error, "Critical Error - Website configuration not found!!");

        var result = _passwordHasher.VerifyHashedPassword(config!, config!.PasswordHash, loginRequestDto.password);

        if(result == PasswordVerificationResult.Failed || config.UserName != loginRequestDto.login)
            return Result<LoginResponseDto>.Failure(ResultStatus.Unauthorized, "Invalid login or password");

        return Result<LoginResponseDto>.Ok(new LoginResponseDto(true, config.UserName, config.Email));
    }
}
