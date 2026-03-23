using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Request;

public sealed record TechnologyUpdateRequestDto(int id, string name, TechnologyCategory category);

