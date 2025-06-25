using cp_journey_backend.Entities;

namespace cp_journey_backend.Persistence;

public class InMemoryDb {

    private List<Profile> _profiles = [];
    private List<University> _universities = [];
    
    public InMemoryDb() {
        
    }

    #region Profile
    
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

    #endregion
    
    #region University
    
    public Task<University?> GetUniversity(string id)
        => Task.FromResult(_universities.FirstOrDefault(u => u.Id == id));
    
    public Task AddUniversity(University university) {
        _universities.Add(university);
        return Task.CompletedTask;
    }
    
    public Task DeleteUniversity(string id) {
        _universities = _universities.Where(u => u.Id != id).ToList();
        return Task.CompletedTask;
    }
    
    public Task UpdateUniversity(University university) {
        DeleteUniversity(university.Id);
        AddUniversity(university);
        return Task.CompletedTask;
    }
    
    #endregion
    
}