using System.Reflection;
using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IProfileService {
    Task<Profile> Add(CreateProfileModel data);
}

public class ProfileService(
    IProfileRepository profileRepository,
    IUniversityRepository universityRepository,
    ModelConverter modelConverter
) : IProfileService {

    public async Task<Profile> Add(CreateProfileModel data) {
        
        var profile = new Profile {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Handle = data.Handle,
            UniversityId = data.UniversityId
        };
        
        await profileRepository.Add(profile);
        await profileRepository.SaveChangesAsync();
        
        return profile;
    }

}