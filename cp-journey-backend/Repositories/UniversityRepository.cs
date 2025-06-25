using cp_journey_backend.Entities;
using cp_journey_backend.Persistence;

namespace cp_journey_backend.Repositories;

public interface IUniversityRepository {
    Task<University?> Get(string id);
}

public class InMemoryUniversityRepository(InMemoryDb dbContext) : IUniversityRepository {
    public Task Add(University university)
        => dbContext.AddUniversity(university);

    public Task Update(University university) 
        => dbContext.UpdateUniversity(university);

    public Task<University?> Get(string id)
        => dbContext.GetUniversity(id);

    public Task Delete(string id)
        => dbContext.DeleteUniversity(id);
}