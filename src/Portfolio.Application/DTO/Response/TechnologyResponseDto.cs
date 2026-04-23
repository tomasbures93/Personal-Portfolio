using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Response;

public sealed record TechnologyResponseDto(
    int Id,
    string Name,
    TechnologyCategory Category);