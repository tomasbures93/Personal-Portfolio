namespace Portfolio.Application.DTO.Response;

public sealed record WebsiteConfigResponseDto(
    string Email,
    List<TechnologyResponseDto> Technologies);