using cp_journey_backend.Entities;
using cp_journey_backend.Persistence;

namespace cp_journey_backend.Repositories;

public interface IProfileRepository {
    
    Task Add(Profile profile);

    Task Update(Profile profile);
    
    Task<Profile?> Get(string id);
    
    Task Delete(string id);

}

public class InMemoryProfileRepository(InMemoryDb dbContext) : IProfileRepository {
    public Task Add(Profile profile)
        => dbContext.AddProfile(profile);

    public Task Update(Profile profile) 
        => dbContext.UpdateProfile(profile);

    public Task<Profile?> Get(string id)
        => dbContext.GetProfile(id);

    public Task Delete(string id)
        => dbContext.DeleteProfile(id);
}