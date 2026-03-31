using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstraction.Persistence;

public interface IWebsiteRepository
{
    Task<WebsiteConfig?> GetWebsiteInfoAsync(CancellationToken token);
    
    Task<WebsiteConfig?> UpdateWebsiteConfig(string newEmail, List<Technology> technologies, CancellationToken token);

    Task<bool> ChangePasswordAsync(string newPassword, CancellationToken token);

    Task<bool> ChangeNameAsync(string newName, CancellationToken token);

}
