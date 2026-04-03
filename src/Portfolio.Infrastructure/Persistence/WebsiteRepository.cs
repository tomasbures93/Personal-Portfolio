using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

public sealed class WebsiteRepository : IWebsiteRepository
{
    private readonly AppDbContext _dbContext;

    public WebsiteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ChangeNameAsync(string newName, CancellationToken token)
    {
        var websiteConfig = await _dbContext.WebsiteConfig.SingleOrDefaultAsync(token);
        if (websiteConfig == null)
            return false;

        websiteConfig.ChangeUserName(newName);

        await _dbContext.SaveChangesAsync(token);

        return true;
    }

    public async Task<bool> ChangePasswordAsync(string newPassword, CancellationToken token)
    {
        var websiteConfig = await _dbContext.WebsiteConfig.SingleOrDefaultAsync(token);
        if (websiteConfig == null)
            return false;

        websiteConfig.UpdatePasswordHash(newPassword);

        await _dbContext.SaveChangesAsync(token);

        return true;
    }

    public async Task<WebsiteConfig?> GetWebsiteInfoAsync(CancellationToken token)
    { 
        return await _dbContext.WebsiteConfig.Include(t => t.Technologies).SingleOrDefaultAsync(token);
    }

    public async Task<WebsiteConfig?> UpdateWebsiteConfig(string newEmail, List<Technology> technologies, CancellationToken token)
    {
        var websiteConfig = await _dbContext.WebsiteConfig.Include(t => t.Technologies).SingleOrDefaultAsync(token);
        if (websiteConfig == null)
            return null;

        websiteConfig.ChangeEmail(newEmail);
        websiteConfig.UpdateTechnologies(technologies);

        await _dbContext.SaveChangesAsync(token);

        return websiteConfig;
    }
}
