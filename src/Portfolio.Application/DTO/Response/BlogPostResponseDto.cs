namespace Portfolio.Application.DTO.Response;

public sealed record BlogPostResponseDto(
    int id,
    string title,
    string content,
    DateTime createdAt,
    DateTime? updatedAt,
    bool draft,
    string creator);

