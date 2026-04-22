namespace Portfolio.Application.DTO.Request;

public sealed record ChangePasswordRequestDto(string OldPassword, string NewPassword);
