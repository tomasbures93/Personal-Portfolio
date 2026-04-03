using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstraction.Persistence;

public interface ITechnologyRepository
{
    Task<Technology?> GetTechnologyAsync(int id, CancellationToken token);

    Task<List<Technology>> GetTechnologiesAsync(CancellationToken token);

    Task<Technology> CreateTechnologyAsync(Technology technology, CancellationToken token);

    Task<Technology?> UpdateTechnologyAsync(Technology technology, CancellationToken token);

    Task<bool> DeleteTechnologyAsync(int id, CancellationToken token);
}
