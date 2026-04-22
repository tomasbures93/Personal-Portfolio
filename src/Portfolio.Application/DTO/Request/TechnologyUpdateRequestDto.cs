using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTO.Request;

public sealed record TechnologyUpdateRequestDto(int Id, string Name, TechnologyCategory Category);

