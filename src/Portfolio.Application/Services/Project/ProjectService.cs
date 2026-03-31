using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Services.Project;

public sealed class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IValidate<int> _validateID;
    private readonly IValidate<ProjectRequestDto> _validateCreateProjectRequest;
    private readonly IValidate<ProjectUpdateRequestDto> _validateProjectUpdateRequest;

    public ProjectService(
        IProjectRepository projectRepository,
        ITechnologyRepository technologyRepository,
        IValidate<int> validateID,
        IValidate<ProjectRequestDto> validateCreateProjectRequest,
        IValidate<ProjectUpdateRequestDto> validateProjectUpdateRequest)
    {
        _projectRepository = projectRepository;
        _technologyRepository = technologyRepository;
        _validateID = validateID;
        _validateCreateProjectRequest = validateCreateProjectRequest;
        _validateProjectUpdateRequest = validateProjectUpdateRequest;
    }

    public async Task<Result<ProjectResponseDto>> CreateProjectAsync(ProjectRequestDto projectRequestDto, CancellationToken token)
    {
        var validationResult = _validateCreateProjectRequest.Validate(projectRequestDto);
        if (!validationResult.IsValid)
            return Result<ProjectResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var technologies = await _technologyRepository.GetTechnologiesAsync(token);
        if (technologies == null || !technologies.Any())
            return Result<ProjectResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB");

        var technologiesToAdd = technologies.Where(i => projectRequestDto.technologies.Contains(i.Id)).ToList();
        if(technologiesToAdd == null || !technologiesToAdd.Any())
            return Result<ProjectResponseDto>.Failure(ResultStatus.Error, "You are trying to add Technologies which are not in DB, create those technologies first");

        var projectModel = new Portfolio.Domain.Entities.Project(
            projectRequestDto.title,
            projectRequestDto.description,
            technologiesToAdd,
            projectRequestDto.url);
        var project = await _projectRepository.CreateProjectAsync(projectModel, token);

        var projectDto = new ProjectResponseDto(
            project.Id,
            project.Title,
            project.Description,
            project.Url,
            project.Technologies.Select(t => new TechnologyResponseDto(
                t.Id,
                t.Name,
                t.Category)).ToList());
        return Result<ProjectResponseDto>.Ok(projectDto);
    }

    public async Task<Result> DeleteProjectAsync(int id, CancellationToken token)
    {
        var validationResult = _validateID.Validate(id);
        if (!validationResult.IsValid)
            return Result.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var isDeleted = await _projectRepository.DeleteProjectAsync(id, token);
        if (!isDeleted)
            return Result.Failure(ResultStatus.NotFound, "Technology not found");

        return Result.Ok();
    }

    public async Task<Result<ProjectResponseDto>> GetProjectAsync(int id, CancellationToken token)
    {
        var validationResult = _validateID.Validate(id);
        if (!validationResult.IsValid)
            return Result<ProjectResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var project = await _projectRepository.GetProjectAsync(id, token);
        if(project == null)
            return Result<ProjectResponseDto>.Failure(ResultStatus.NotFound, "Project not found");

        var projectDto = new ProjectResponseDto(
            project.Id,
            project.Title,
            project.Description,
            project.Url,
            project.Technologies.Select(t => new TechnologyResponseDto(
                t.Id,
                t.Name,
                t.Category)).ToList());

        return Result<ProjectResponseDto>.Ok(projectDto);
    }

    public async Task<Result<List<ProjectResponseDto>>> GetProjectsAsync(CancellationToken token)
    {
        var projects = await _projectRepository.GetProjectsAsync(token);

        var projectDto = projects
            .Select(p => new ProjectResponseDto(
                p.Id,
                p.Title,
                p.Description,
                p.Url,
                p.Technologies.Select(t => new TechnologyResponseDto(
                    t.Id,
                    t.Name,
                    t.Category)).ToList()))
            .ToList();

        return Result<List<ProjectResponseDto>>.Ok(projectDto);
    }

    public async Task<Result<ProjectResponseDto>> UpdateProjectAsync(ProjectUpdateRequestDto projectUpdateRequestDto, CancellationToken token)
    {
        var validationResult = _validateProjectUpdateRequest.Validate(projectUpdateRequestDto);
        if (!validationResult.IsValid)
            return Result<ProjectResponseDto>.Failure(ResultStatus.ValidationError, validationResult.Errors);

        var technologies = await _technologyRepository.GetTechnologiesAsync(token);
        if (technologies == null || !technologies.Any())
            return Result<ProjectResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB");

        var technologiesToUpdate = technologies.Where(i => projectUpdateRequestDto.technologies.Contains(i.Id)).ToList();
        if (technologiesToUpdate == null || !technologiesToUpdate.Any())
            return Result<ProjectResponseDto>.Failure(ResultStatus.Error, "No technologies found in DB, add some first and try again");

        var projectModel = new Portfolio.Domain.Entities.Project(
            projectUpdateRequestDto.id,
            projectUpdateRequestDto.title,
            projectUpdateRequestDto.description,
            technologiesToUpdate,
            projectUpdateRequestDto.url);
        var project = await _projectRepository.UpdateProjectAsync(projectModel, token);
        if (project == null)
            return Result<ProjectResponseDto>.Failure(ResultStatus.NotFound, "Project not found");

        var projectDto = new ProjectResponseDto(
            project.Id,
            project.Title,
            project.Description,
            project.Url,
            project.Technologies.Select(t => new TechnologyResponseDto(
                t.Id,
                t.Name,
                t.Category)).ToList());
        return Result<ProjectResponseDto>.Ok(projectDto);
    }
}
