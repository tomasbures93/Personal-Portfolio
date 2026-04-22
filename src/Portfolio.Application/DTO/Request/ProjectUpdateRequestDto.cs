namespace Portfolio.Application.DTO.Request;

public sealed record ProjectUpdateRequestDto(int Id, string Title, string Description, List<int> Technologies, string? Url);
