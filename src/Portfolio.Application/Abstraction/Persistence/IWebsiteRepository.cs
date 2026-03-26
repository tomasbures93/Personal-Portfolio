using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstraction.Persistence;

public interface IWebsiteRepository
{
    Task<WebsiteConfig> GetWebsiteInfoAsync(CancellationToken token);
    
    Task<WebsiteConfig> UpdateWebsiteConfig(WebsiteConfig config, CancellationToken token);

    Task<bool> ChangePasswordAsync(string newPassword, CancellationToken token);

}
