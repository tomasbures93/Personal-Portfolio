namespace Portfolio.Application.DTO.Response;

public sealed record LoginResponseDto(bool Success, string UserName, string Email);

