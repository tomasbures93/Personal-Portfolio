using Microsoft.AspNetCore.Identity;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services.Website;

public class WebsiteService : IWebsiteService
{
    private readonly IWebsiteRepository _repository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IValidate<ChangeNameRequestDto> _validateChangeName;
    private readonly IValidate<ChangePasswordRequestDto> _validateChangePassword;
    private readonly IValidate<WebsiteConfigUpdateRequestDto> _validateConfigUpdate;
    private readonly IPasswordHasher<WebsiteConfig> _passwordHasher;

    public WebsiteService(
        IWebsiteRepository repository,
        ITechnologyRepository technologyRepository,
        IValidate<ChangeNameRequestDto> validateChangeName,
        IValidate<ChangePasswordRequestDto> validateChangePassword,
        IValidate<WebsiteConfigUpdateRequestDto> validateConfigUpdate,
        IPasswordHasher<WebsiteConfig> passwordHasher)
    {
        _repository = repository;
        _technologyRepository = technologyRepository;
        _validateChangeName = validateChangeName;
        _validateChangePassword = validateChangePassword;
        _validateConfigUpdate = validateConfigUpdate;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> ChangeNameAsync(ChangeNameRequestDto changeNameRequestDto, CancellationToken token)
    {
        var validationResult = _validateChangeName.Validate(changeNameRequestDto);
        if (!validationResult.IsValid)
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var response = await _repository.ChangeNameAsync(changeNameRequestDto.name, token);
        if (response == false)
            return Result.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");

        return Result.Ok();
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequestDto, CancellationToken token)
    {
        var validationResult = _validateChangePassword.Validate(changePasswordRequestDto);
        if (!validationResult.IsValid)
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var config = await _repository.GetWebsiteInfoAsync(token);
        if (config == null)
            return Result.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(config, config.PasswordHash, changePasswordRequestDto.oldPassword);
        if(passwordVerificationResult == PasswordVerificationResult.Failed)
            return Result.Failure(ResultStatus.Unauthorized, "Old password is incorrect!");

        var hashedPassword = _passwordHasher.HashPassword(config, changePasswordRequestDto.newPassword);

        var response = await _repository.ChangePasswordAsync(hashedPassword, token);
        if(response == false)
            return Result.Failure(ResultStatus.Error, "Critical Error! Password could not be updated.");

        return Result.Ok();
    }

    public async Task<Result<WebsiteConfigResponseDto>> GetWebsiteInfoAsync(CancellationToken token)
    {
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
        var validationResult = _validateConfigUpdate.Validate(updateRequestDto);
        if (!validationResult.IsValid)
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var technologies = await _technologyRepository.GetTechnologiesAsync(token);
        if(technologies == null || !technologies.Any())
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB");

        var technologiesToUpdate = technologies.Where(t => updateRequestDto.technologies.Contains(t.Id)).ToList();
        if (technologiesToUpdate == null || !technologiesToUpdate.Any())
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB, add some first and try again");

        var response = await _repository.UpdateWebsiteConfig(
            updateRequestDto.email, 
            technologiesToUpdate, 
            token);
        if (response == null)
            return Result<WebsiteConfigResponseDto>.Failure(ResultStatus.Error, "Critical Error! Website Config not found!");

        var configUpdateDto = new WebsiteConfigResponseDto(
            response.Email,
            response.Technologies.Select(t => new TechnologyResponseDto(
                t.Id, 
                t.Name, 
                t.Category)).ToList());
        return Result<WebsiteConfigResponseDto>.Ok(configUpdateDto);
    }
}
