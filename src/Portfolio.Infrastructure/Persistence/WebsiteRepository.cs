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

    public async Task<bool> ChangePasswordAsync(string newPassword, CancellationToken token)
    {
        var websiteConfig = await _dbContext.WebsiteConfig.SingleOrDefaultAsync(token);

        websiteConfig!.UpdatePassword(newPassword);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<WebsiteConfig> GetWebsiteInfoAsync(CancellationToken token)
    { 
        return await _dbContext.WebsiteConfig.SingleOrDefaultAsync(token);
    }

    public async Task<WebsiteConfig> UpdateWebsiteConfig(WebsiteConfig config, CancellationToken token)
    {
        var websiteConfig = await _dbContext.WebsiteConfig.SingleOrDefaultAsync(token);

        websiteConfig!.ChangeEmail(config.Email);
        websiteConfig!.ChangeUserName(config.UserName);
        websiteConfig!.UpdateTechnologies((List<Technology>)config.Technologies);

        await _dbContext.SaveChangesAsync();

        return websiteConfig;
    }
}
