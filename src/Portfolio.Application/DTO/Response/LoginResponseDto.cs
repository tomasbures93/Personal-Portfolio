namespace Portfolio.Application.DTO.Response;

public sealed record LoginResponseDto(bool success, string userName, string email);

