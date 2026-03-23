using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Response;

public sealed record TechnologyResponseDto(int id, string name, TechnologyCategory category);
