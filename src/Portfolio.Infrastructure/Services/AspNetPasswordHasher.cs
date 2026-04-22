using Microsoft.AspNetCore.Identity;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Common.Results;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services;

public class AspNetPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<WebsiteConfig> _passwordHasher;

    public AspNetPasswordHasher()
    {
        _passwordHasher = new PasswordHasher<WebsiteConfig>();
    }

    public Result<string> HashPassword(string password)
    {
        return Result<string>.Ok(_passwordHasher.HashPassword(null!, password));
    }

    public Result Verify(string hash, string password)
    {
        var result =_passwordHasher.VerifyHashedPassword(null!, hash, password);
        if (result == PasswordVerificationResult.Success) 
        { 
            return Result.Ok();
        } else 
        { 
            return Result.Failure(ResultStatus.Unauthorized, "Invalid password");
        }
    }
}
