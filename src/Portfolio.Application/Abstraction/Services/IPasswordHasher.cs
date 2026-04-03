using Portfolio.Application.Common.Results;

namespace Portfolio.Application.Abstraction.Services;

public interface IPasswordHasher
{
    Task<Result<string>> HashPassword(string password);

    Task<Result> CheckPassword(string password, string dbPassword);
}
