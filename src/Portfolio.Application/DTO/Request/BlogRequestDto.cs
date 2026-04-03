namespace Portfolio.Application.DTO.Request;

public sealed record BlogRequestDto(
    string title,
    string content,
    bool draft);
