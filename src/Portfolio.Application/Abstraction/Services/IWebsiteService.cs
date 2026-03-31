using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Abstraction.Services;

public interface IWebsiteService
{
    Task<Result<WebsiteConfigResponseDto>> GetWebsiteInfoAsync(CancellationToken token);

    Task<Result> ChangePasswordAsync(ChangePasswordRequestDto newPassword, CancellationToken token);

    Task<Result> ChangeNameAsync(ChangeNameRequestDto newName, CancellationToken token);

    Task<Result<WebsiteConfigResponseDto>> UpdateWebsiteConfig(WebsiteConfigUpdateRequestDto config, CancellationToken token);

}
