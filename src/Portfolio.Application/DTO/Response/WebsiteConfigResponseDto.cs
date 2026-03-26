using Portfolio.Domain.Entities;

namespace Portfolio.Application.DTO.Response;

public sealed record WebsiteConfigResponseDto(
    string email,
    List<Technology> technologies);
