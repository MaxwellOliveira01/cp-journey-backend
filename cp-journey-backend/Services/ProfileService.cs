using System.Reflection;
using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IProfileService {
    Task<Profile> AddAsync(CreateProfileModel data);
    Task<Profile> UpdateAsync(UpdateProfileModel data);
}

public class ProfileService(
    IProfileRepository profileRepository,
    IUniversityRepository universityRepository,
    ModelConverter modelConverter
) : IProfileService {

    public async Task<Profile> AddAsync(CreateProfileModel data) {
        
        var profile = new Profile {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Handle = data.Handle,
            UniversityId = data.UniversityId
        };
        
        await profileRepository.AddAsync(profile);
        return profile;
    }
    
    public async Task<Profile> UpdateAsync(UpdateProfileModel data) {
        var profile = await profileRepository.GetRequiredAsync(data.Id);
        
        profile.Name = data.Name;
        profile.Handle = data.Handle;
        profile.UniversityId = data.UniversityId;

        await profileRepository.UpdateAsync(profile);
        
        return profile;
    }

}