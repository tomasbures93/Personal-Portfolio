using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Configuration;

public sealed class PortfolioConfig
{
    [Required]
    [MinLength(10, ErrorMessage = "Password has to be atleast 10 Characters long")]
    public string AdminPassword { get; init; } = string.Empty;

    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+(\.[^@\s]{2,})+$", ErrorMessage = "Use a valid email like something@example.com")]
    public string AdminEmail { get; init; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "AdminUserName has to be atleast 6 Characters long")]
    public string AdminUserName { get; init; } = string.Empty;
}
