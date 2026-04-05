namespace Portfolio.Application.DTO.Request;

public sealed record ChangePasswordRequestDto(string oldPassword, string newPassword);
