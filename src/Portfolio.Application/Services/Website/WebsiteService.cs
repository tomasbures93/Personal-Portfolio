using Microsoft.Extensions.Logging;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Services.Website;

public sealed class WebsiteService : IWebsiteService
{
    private readonly ILogger<WebsiteService> _logger;
    private readonly IWebsiteRepository _repository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IValidate<ChangeNameRequestDto> _validateChangeName;
    private readonly IValidate<ChangePasswordRequestDto> _validateChangePassword;
    private readonly IValidate<WebsiteConfigUpdateRequestDto> _validateConfigUpdate;
    private readonly IPasswordHasher _passwordHasher;

    public WebsiteService(
        IWebsiteRepository repository,
        ITechnologyRepository technologyRepository,
        IValidate<ChangeNameRequestDto> validateChangeName,
        IValidate<ChangePasswordRequestDto> validateChangePassword,
        IValidate<WebsiteConfigUpdateRequestDto> validateConfigUpdate,
        IPasswordHasher passwordHasher,
        ILogger<WebsiteService> logger)
    {
        _repository = repository;
        _technologyRepository = technologyRepository;
        _validateChangeName = validateChangeName;
        _validateChangePassword = validateChangePassword;
        _validateConfigUpdate = validateConfigUpdate;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result> ChangeNameAsync(ChangeNameRequestDto changeNameRequestDto, CancellationToken token)
    {
        _logger.LogInformation("Changing admin username. NewUsername: {NewUsername}", changeNameRequestDto.Name);
        var validationResult = _validateChangeName.Validate(changeNameRequestDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Admin username change validation failed. Errors: {Errors}", string.Join(", ", validationResult.Errors));
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);
        }

        var response = await _repository.ChangeNameAsync(changeNameRequestDto.Name, token);
        if (response == false)
        {
            _logger.LogError("Critical error - Website config not found during username change");
            return Result.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");
        }

        _logger.LogInformation("Admin username changed successfully. NewUsername: {NewUsername}", changeNameRequestDto.Name);
        return Result.Ok();
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequestDto, CancellationToken token)
    {
        _logger.LogInformation("Admin password change requested");
        var validationResult = _validateChangePassword.Validate(changePasswordRequestDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Admin password change validation failed. Errors: {Errors}", string.Join(", ", validationResult.Errors));
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);
        }

        var config = await _repository.GetWebsiteInfoAsync(token);
        if (config == null)
        {
            _logger.LogError("Critical error - Website config not found during password change attempt");
            return Result.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");
        }

        var passwordVerificationResult = _passwordHasher.Verify(config.PasswordHash, changePasswordRequestDto.OldPassword);
        if(!passwordVerificationResult.IsSuccess)
        {
            _logger.LogWarning("Admin password change failed - Old password verification failed");
            return Result.Failure(ResultStatus.Unauthorized, "Old password is incorrect!");
        }

        var hashedPassword = _passwordHasher.HashPassword(changePasswordRequestDto.NewPassword);
        if (!hashedPassword.IsSuccess)
        {
            _logger.LogError("Failed to hash new password");
            return Result.Failure(ResultStatus.Error, "Password hashing failed");
        }

        var response = await _repository.ChangePasswordAsync(hashedPassword.Value, token);
        if(response == false)
        {
            _logger.LogError("Critical error - Admin password could not be updated in database");
            return Result.Failure(ResultStatus.Error, "Critical Error! Password could not be updated.");
        }

        _logger.LogInformation("Admin password changed successfully");
        return Result.Ok();
    }

    public async Task<Result<WebsiteConfigResponseDto>> GetWebsiteInfoAsync(CancellationToken token)
    {
        _logger.LogInformation("GetWebsiteInfoAsync called");
        var config = await _repository.GetWebsiteInfoAsync(token);
        if (config == null)
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");


        var responseDto = new WebsiteConfigResponseDto(
            config.Email, 
            config.Technologies.Select(t => new TechnologyResponseDto(
                t.Id, 
                t.Name, 
                t.Category)).ToList());

        return Result<WebsiteConfigResponseDto>.Ok(responseDto);
    }

    public async Task<Result<WebsiteConfigResponseDto>> UpdateWebsiteConfig(WebsiteConfigUpdateRequestDto updateRequestDto, CancellationToken token)
    {
        _logger.LogInformation("Updating website config. Email: {Email}, TechnologyCount: {Count}", updateRequestDto.Email, updateRequestDto.Technologies?.Count ?? 0);
        var validationResult = _validateConfigUpdate.Validate(updateRequestDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Website config update validation failed. Errors: {Errors}", string.Join(", ", validationResult.Errors));
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);
        }

        var technologies = await _technologyRepository.GetTechnologiesAsync(token);
        if(technologies == null || !technologies.Any())
        {
            _logger.LogError("Critical error - No technologies found in database during config update");
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB");
        }

        var technologiesToUpdate = technologies.Where(t => updateRequestDto.Technologies.Contains(t.Id)).ToList();
        if (technologiesToUpdate == null || !technologiesToUpdate.Any())
        {
            _logger.LogWarning("No matching technologies found for update request. RequestedTechnologyIds: {TechIds}", string.Join(", ", updateRequestDto.Technologies));
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB, add some first and try again");
        }

        var response = await _repository.UpdateWebsiteConfig(
            updateRequestDto.Email, 
            technologiesToUpdate, 
            token);
        if (response == null)
        {
            _logger.LogError("Critical error - Website config not found during update");
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");
        }

        _logger.LogInformation("Website config updated successfully. Email: {Email}, UpdatedTechnologies: {Count}", response.Email, response.Technologies.Count);

        var configUpdateDto = new WebsiteConfigResponseDto(
            response.Email,
            response.Technologies.Select(t => new TechnologyResponseDto(
                t.Id, 
                t.Name, 
                t.Category)).ToList());
        return Result<WebsiteConfigResponseDto>.Ok(configUpdateDto);
    }
}
