using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

public sealed class TechnologyRepository : ITechnologyRepository
{
    private readonly AppDbContext _dbContext;

    public TechnologyRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<Technology> CreateTechnologyAsync(Technology technology, CancellationToken token)
    {
        await _dbContext.Technology.AddAsync(technology, token);
        await _dbContext.SaveChangesAsync(token);

        return technology;
    }

    public async Task<bool> DeleteTechnologyAsync(int id, CancellationToken token)
    {
        var technology = await _dbContext.Technology.SingleOrDefaultAsync(x => x.Id == id, token);
        if (technology is null)
            return false;

        _dbContext.Remove(technology);
        await _dbContext.SaveChangesAsync(token);

        return true;
    }

    public async Task<List<Technology>> GetTechnologiesAsync(CancellationToken token)
    {
        return await _dbContext.Technology.ToListAsync(token);
    }

    public async Task<Technology?> GetTechnologyAsync(int id, CancellationToken token)
    {
        return await _dbContext.Technology.SingleOrDefaultAsync(t => t.Id == id, token);
    }

    public async Task<Technology?> UpdateTechnologyAsync(Technology technology, CancellationToken token)
    {
        var existingTechnology = await _dbContext.Technology.SingleOrDefaultAsync(t => t.Id == technology.Id, token);
        if (existingTechnology is null)
            return null;

        existingTechnology.UpdateName(technology.Name);
        existingTechnology.UpdateCategory(technology.Category);

        await _dbContext.SaveChangesAsync(token);

        return existingTechnology;
    }
}
