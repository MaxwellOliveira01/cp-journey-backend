using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("profile")]
public class ProfileController(
    IProfileService profileService,
    IProfileRepository profileRepository,
    IUniversityRepository universityRepository,
    ModelConverter modelConverter)
: ControllerBase {

    [HttpGet("{id}")]
    public async Task<ProfileModel> GetAsync(Guid id) {
        var profile = await profileRepository.GetRequiredAsync(id);
        
        var university = profile.UniversityId.HasValue 
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        return modelConverter.ToModel(profile, university);
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<ProfileModel>> ListAsync() {
        var profiles = await profileRepository.ListAsync();
        // Tudo isso aqui pq eu coloquei ProfileModel.UniversityModel
        // e preciso de uma query a mais pra pegar esse dado
        // TODO: tirar esse universityModel do ProfileModel
        //    Ou fazer com um join dentro do repository e alterar o modelConverter
        var tasks = profiles.Select(async p => {
            var university = p.UniversityId.HasValue
                ? await universityRepository.GetAsync(p.UniversityId.Value)
                : null;
            return modelConverter.ToModel(p, university);
        });
        var models = await Task.WhenAll(tasks);
        return models.ToList();
    }
    
    [HttpPost]
    public async Task<ProfileModel> CreateAsync(CreateProfileModel data) {
        var profile = await profileService.AddAsync(data);
        
        var university = profile.UniversityId.HasValue
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        return modelConverter.ToModel(profile, university);
    }

    [HttpPut]
    public async Task<ProfileModel> UpdateAsync(UpdateProfileModel data) {
        var profile = await profileService.UpdateAsync(data);

        var university = profile.UniversityId.HasValue
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        return modelConverter.ToModel(profile, university);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id) {
        var profile = await profileRepository.GetRequiredAsync(id);
        await profileRepository.DeleteAsync(profile);
        return NoContent(); // 204 (Ok)
    }
    
    
}