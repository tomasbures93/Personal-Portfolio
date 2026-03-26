using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Abstraction.Services;

public interface IProjectService
{
    Task<Result<ProjectResponseDto>> GetProjectAsync(int id, CancellationToken token);

    Task<Result<List<ProjectResponseDto>>> GetProjectsAsync(CancellationToken token);

    Task<Result<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto projectRequestDto, CancellationToken token);

    Task<Result<ProjectResponseDto>> UpdateProjectAsync(ProjectUpdateRequestDto projectUpdateRequestDto, CancellationToken token);

    Task<Result> DeleteProjectAsync(int id, CancellationToken token);
}
