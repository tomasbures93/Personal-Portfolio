using Portfolio.Application.Common.Results;

namespace Portfolio.Application.Abstraction.Services;

public interface IPasswordHasher
{
    Result<string> HashPassword(string password);

    Result Verify(string hash, string password);
}
