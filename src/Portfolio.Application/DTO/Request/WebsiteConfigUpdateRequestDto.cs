namespace Portfolio.Application.DTO.Request;

public sealed record WebsiteConfigUpdateRequestDto(
    string email,
    List<int> technologies);
