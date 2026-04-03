namespace Portfolio.Application.DTO.Request;

public sealed record BlogUpdateRequestDto(
    int id,
    string title,
    string content,
    bool draft);
