using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;

namespace Portfolio.Application.Abstraction.Services;

public interface IWebsiteService
{
    Task<Result> GetWebsiteInfoAsync(CancellationToken token);

    Task<Result> ChangePasswordAsync(string newPassword, CancellationToken token);

    Task<Result> UpdateWebsiteConfig(WebsiteConfigUpdateRequestDto config, CancellationToken token);

}
