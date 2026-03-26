using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

public sealed class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;

    public ProjectRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Project> CreateProjectAsync(Project project, CancellationToken token)
    {
        await _dbContext.Projects.AddAsync(project, token);
        await _dbContext.SaveChangesAsync();

        return project;
    }

    public async Task<bool> DeleteProjectAsync(int id, CancellationToken token)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id, token);
        if (project == null)
            return false;

        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Project?> GetProjectAsync(int id, CancellationToken token)
    {
        return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id, token);
    }

    public async Task<List<Project>> GetProjectsAsync(CancellationToken token)
    {
        return await _dbContext.Projects.ToListAsync(token);
    }

    public async Task<Project?> UpdateProjectAsync(Project project, CancellationToken token)
    {
        var existingProject = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == project.Id, token);
        if (existingProject == null)
            return null;

        existingProject.UpdateTitle(project.Title);
        existingProject.UpdateDescription(project.Description);
        existingProject.UpdateUrl(project.Url);
        existingProject.UpdateTechnologies((List<Technology>)project.Technologies);

        await _dbContext.SaveChangesAsync();

        return existingProject;
    }
}
