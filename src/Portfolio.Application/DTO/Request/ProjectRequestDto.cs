namespace Portfolio.Application.DTO.Request;

public sealed record ProjectRequestDto(string Title, string Description, List<int> Technologies, string? Url);