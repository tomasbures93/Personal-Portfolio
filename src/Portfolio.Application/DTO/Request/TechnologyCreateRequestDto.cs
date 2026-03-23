using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Request;

public sealed record TechnologyCreateRequestDto(string name, TechnologyCategory category);

