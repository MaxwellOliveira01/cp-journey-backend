using cp_journey_backend.Entities;

namespace cp_journey_backend.Persistence;

public class InMemoryDatabaseRepository {

    private List<Profile> _profiles = [];
    
    public InMemoryDatabaseRepository() {
        
    }

    public Task<Profile?> GetProfile(string id)
        => Task.FromResult(_profiles.FirstOrDefault(p => p.Id == id));

    public Task AddProfile(Profile profile) {
        _profiles.Add(profile);
        return Task.CompletedTask;
    }
    
    public Task DeleteProfile(string id) {
        _profiles = _profiles.Where(p => p.Id != id).ToList();
        return Task.CompletedTask;
    }

    public Task UpdateProfile(Profile profile) {
        DeleteProfile(profile.Id);
        AddProfile(profile);
        return Task.CompletedTask;
    }

}