namespace Portfolio.Application.DTO.Request;

public sealed record ProjectUpdateRequestDto(int id, string title, string description, List<int> technologies, string? url);
