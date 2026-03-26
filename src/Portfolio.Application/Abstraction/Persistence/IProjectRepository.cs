using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstraction.Persistence;

public interface IProjectRepository
{
    Task<Project?> GetProjectAsync(int id, CancellationToken token);

    Task<List<Project>> GetProjectsAsync(CancellationToken token);

    Task<Project> CreateProjectAsync(Project project, CancellationToken token);

    Task<Project?> UpdateProjectAsync(Project project, CancellationToken token);

    Task<bool> DeleteProjectAsync(int id, CancellationToken token);
}
