using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Services.Technology;

public class TechnologyService : ITechnologyService
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IValidate<int> _validateTechnologyID;
    private readonly IValidate<TechnologyCreateRequestDto> _validateTechnologyCreateRequest;
    private readonly IValidate<TechnologyUpdateRequestDto> _validateTechnologyUpdateRequest;

    public TechnologyService(
        ITechnologyRepository technologyRepository, 
        IValidate<int> validateTechnologyID, 
        IValidate<TechnologyCreateRequestDto> validateTechnologyCreateRequest,
        IValidate<TechnologyUpdateRequestDto> validateTechnologyUpdateRequest)
    {
        _technologyRepository = technologyRepository;
        _validateTechnologyID = validateTechnologyID;
        _validateTechnologyCreateRequest = validateTechnologyCreateRequest;
        _validateTechnologyUpdateRequest = validateTechnologyUpdateRequest;
    }

    public async Task<Result<TechnologyResponseDto>> CreateTechnologyAsync(TechnologyCreateRequestDto technologyCreateRequestDto, CancellationToken token)
    {
        var validationResult = _validateTechnologyCreateRequest.Validate(technologyCreateRequestDto);
        if (!validationResult.IsValid)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var technologyModel = new Portfolio.Domain.Entities.Technology(technologyCreateRequestDto.name, technologyCreateRequestDto.category);

        var technology = await _technologyRepository.CreateTechnologyAsync(technologyModel, token);
        var techDto = new TechnologyResponseDto(technology.Id, technology.Name, technology.Category);

        return Result<TechnologyResponseDto>.Ok(techDto);
    }

    public async Task<Result> DeleteTechnologyAsync(int id, CancellationToken token)
    {
        var validationResult = _validateTechnologyID.Validate(id);
        if (!validationResult.IsValid)
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var deleteResult = await _technologyRepository.DeleteTechnologyAsync(id, token);
        if(!deleteResult)
            return Result.Failure(ResultStatus.NotFound, "Technology not found");

        return Result.Ok();
    }

    public async Task<Result<List<TechnologyResponseDto>>> GetTechnologiesAsync(CancellationToken token)
    {
        var technologies = await _technologyRepository.GetTechnologiesAsync(token);

        var technologiesDto = technologies.Select(t => new TechnologyResponseDto(t.Id, t.Name, t.Category)).ToList();

        return Result<List<TechnologyResponseDto>>.Ok(technologiesDto);
    }

    public async Task<Result<TechnologyResponseDto>> GetTechnologyAsync(int id, CancellationToken token)
    {
        var validationResult = _validateTechnologyID.Validate(id);
        if (!validationResult.IsValid)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var getResult = await _technologyRepository.GetTechnologyAsync(id, token);
        if (getResult == null)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.NotFound, "Technology not found");

        var technologyDto = new TechnologyResponseDto(getResult.Id, getResult.Name, getResult.Category);

        return Result<TechnologyResponseDto>.Ok(technologyDto);

    }

    public Task<Result<TechnologyResponseDto>> UpdateTechnologyAsync(TechnologyUpdateRequestDto technologyUpdateRequestDto, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
