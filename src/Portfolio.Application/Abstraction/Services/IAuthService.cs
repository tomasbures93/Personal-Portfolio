using Portfolio.Application.Common.Results;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.DTO.Response;

namespace Portfolio.Application.Abstraction.Services;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken token);
}
