namespace Portfolio.Application.DTO.Request;

public sealed record WebsiteConfigUpdateRequestDto(
    string Email,
    List<int> Technologies);