using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Request;

public sealed record TechnologyRequestDto(string name, TechnologyCategory category);

