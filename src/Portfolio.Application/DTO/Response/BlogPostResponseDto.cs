namespace Portfolio.Application.DTO.Response;

public sealed record BlogPostResponseDto(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool Draft,
    string Creator);

