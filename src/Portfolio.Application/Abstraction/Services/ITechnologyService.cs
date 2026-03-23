using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Abstraction.Services;

public interface ITechnologyService
{
    Task<Result<TechnologyResponseDto>> GetTechnologyAsync(int id, CancellationToken token);

    Task<Result<List<TechnologyResponseDto>>> GetTechnologiesAsync(CancellationToken token);

    Task<Result<TechnologyResponseDto>> CreateTechnologyAsync(TechnologyCreateRequestDto technologyCreateRequestDto, CancellationToken token);

    Task<Result<TechnologyResponseDto>> UpdateTechnologyAsync(TechnologyUpdateRequestDto technologyUpdateRequestDto, CancellationToken token);

    Task<Result> DeleteTechnologyAsync(int id, CancellationToken token);
}
