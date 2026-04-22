namespace Portfolio.Application.DTO.Request;

public sealed record BlogUpdateRequestDto(
    int Id,
    string Title,
    string Content,
    bool Draft);