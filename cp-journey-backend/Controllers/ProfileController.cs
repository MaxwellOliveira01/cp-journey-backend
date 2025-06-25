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
    public async Task<ProfileModel> Get(string id) {
        var profile = (await profileRepository.Get(id))!;
        var university = await universityRepository.Get(profile.UniversityId);
        return modelConverter.ToModel(profile, university);
    }
    
    [HttpPost]
    public async Task<ProfileModel> Create(CreateProfileModel data) {
        var profile = await profileService.Add(data);
        var university = await universityRepository.Get(data.UniversityId);
        return modelConverter.ToModel(profile, university);
    }
    
}