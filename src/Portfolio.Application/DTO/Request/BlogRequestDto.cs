namespace Portfolio.Application.DTO.Request;

public sealed record BlogRequestDto(
    string Title,
    string Content,
    bool Draft);
