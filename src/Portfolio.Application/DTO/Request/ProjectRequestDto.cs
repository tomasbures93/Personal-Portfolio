namespace Portfolio.Application.DTO.Request;

public sealed record ProjectRequestDto(string title, string description, List<int> technologies, string? url);