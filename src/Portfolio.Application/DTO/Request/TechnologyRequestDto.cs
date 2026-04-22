using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Request;

public sealed record TechnologyRequestDto(string Name, TechnologyCategory Category);

