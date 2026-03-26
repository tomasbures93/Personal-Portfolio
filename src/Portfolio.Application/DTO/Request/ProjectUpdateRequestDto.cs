namespace Portfolio.Application.DTO.Request;

public sealed record ProjectUpdateRequestDto(int id, string title, string description, List<TechnologyUpdateRequestDto> technologies, string? url);
