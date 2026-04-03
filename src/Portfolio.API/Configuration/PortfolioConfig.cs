using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Configuration;

public sealed class PortfolioConfig()
{
    [Required]
    public string AdminPassword { get; init; } = string.Empty;

    [Required]
    public string AdminEmail { get; init; } = string.Empty;

    [Required]
    public string AdminUserName { get; init; } = string.Empty;
}
