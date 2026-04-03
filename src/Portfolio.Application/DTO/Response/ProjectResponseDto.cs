using Portfolio.Domain.Entities;

namespace Portfolio.Application.DTO.Response;

public sealed record ProjectResponseDto(
    int id, 
    string Title, 
    string Description, 
    string Url, 
    List<TechnologyResponseDto> Technologies);
