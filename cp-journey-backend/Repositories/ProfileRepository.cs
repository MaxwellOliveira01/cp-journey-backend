using cp_journey_backend.Api;
using cp_journey_backend.Entities;

namespace cp_journey_backend.Services;

public interface IProfileRepository {

    Task<Profile> Add(CreateProfileModel data);

    Task<Profile> Update(UpdateProfileModel data);
    
    Task<Profile> Get(string id);
    
    Task Delete(string id);

}

public class ProfileRepository : IProfileRepository {

    public ProfileRepository() {
        
    }

    public Task<Profile> Add(CreateProfileModel data) {
        throw new NotImplementedException();
    }

    public Task<Profile> Update(UpdateProfileModel data) {
        throw new NotImplementedException();
    }

    public Task<Profile> Get(string id) {
        throw new NotImplementedException();
    }

    public Task Delete(string id) {
        throw new NotImplementedException();
    }
}