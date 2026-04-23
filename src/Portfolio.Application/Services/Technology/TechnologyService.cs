using Portfolio.Application.Abstraction.Mapper;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Services.Technology;

public sealed class TechnologyService : ITechnologyService
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IValidate<int> _validateID;
    private readonly IValidate<TechnologyRequestDto> _validateTechnologyCreateRequest;
    private readonly IValidate<TechnologyUpdateRequestDto> _validateTechnologyUpdateRequest;
    private readonly IObjectMapper _mapper;

    public TechnologyService(
        ITechnologyRepository technologyRepository, 
        IValidate<int> validateID, 
        IValidate<TechnologyRequestDto> validateTechnologyCreateRequest,
        IValidate<TechnologyUpdateRequestDto> validateTechnologyUpdateRequest,
        IObjectMapper mapper)
    {
        _technologyRepository = technologyRepository;
        _validateID = validateID;
        _validateTechnologyCreateRequest = validateTechnologyCreateRequest;
        _validateTechnologyUpdateRequest = validateTechnologyUpdateRequest;
        _mapper = mapper;
    }

    public async Task<Result<TechnologyResponseDto>> CreateTechnologyAsync(TechnologyRequestDto technologyCreateRequestDto, CancellationToken token)
    {
        var validationResult = _validateTechnologyCreateRequest.Validate(technologyCreateRequestDto);
        if (!validationResult.IsValid)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var technologyModel = new Portfolio.Domain.Entities.Technology(
            technologyCreateRequestDto.Name, 
            technologyCreateRequestDto.Category);

        var technology = await _technologyRepository.CreateTechnologyAsync(technologyModel, token);

        var techDto = new TechnologyResponseDto(technology.Id, technology.Name, technology.Category);
        return Result<TechnologyResponseDto>.Ok(
               _mapper.Map<Portfolio.Domain.Entities.Technology, TechnologyResponseDto>(technologyModel)
               );
    }

    public async Task<Result> DeleteTechnologyAsync(int id, CancellationToken token)
    {
        var validationResult = _validateID.Validate(id);
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

        var technologiesDto = technologies.Select(t => new TechnologyResponseDto(t.Id, t.Name, t.Category))
            .OrderBy(t => t.Id).ToList();
        return Result<List<TechnologyResponseDto>>.Ok(technologiesDto);
    }

    public async Task<Result<TechnologyResponseDto>> GetTechnologyAsync(int id, CancellationToken token)
    {
        var validationResult = _validateID.Validate(id);
        if (!validationResult.IsValid)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var getResult = await _technologyRepository.GetTechnologyAsync(id, token);
        if (getResult == null)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.NotFound, "Technology not found");

        var technologyDto = new TechnologyResponseDto(getResult.Id, getResult.Name, getResult.Category);
        return Result<TechnologyResponseDto>.Ok(technologyDto);

    }

    public async Task<Result<TechnologyResponseDto>> UpdateTechnologyAsync(TechnologyUpdateRequestDto technologyUpdateRequestDto, CancellationToken token)
    {
        var validationResult = _validateTechnologyUpdateRequest.Validate(technologyUpdateRequestDto);
        if (!validationResult.IsValid)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var technologyModel = new Portfolio.Domain.Entities.Technology(
            technologyUpdateRequestDto.Id, 
            technologyUpdateRequestDto.Name, 
            technologyUpdateRequestDto.Category);
        var technology = await _technologyRepository.UpdateTechnologyAsync(technologyModel, token);
        if (technology == null)
            return Result<TechnologyResponseDto>.Failure(ResultStatus.NotFound, "Technology not found");

        var technologyDto = new TechnologyResponseDto(technology.Id, technology.Name, technology.Category);
        return Result<TechnologyResponseDto>.Ok(technologyDto);
    }
}
