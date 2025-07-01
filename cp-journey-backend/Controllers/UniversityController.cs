using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("university")]
public class UniversityController(
    IUniversityRepository universityRepository,
    IUniversityService universityService,
    IPersonRepository personRepository,
    ITeamRepository teamRepository,
    ILocalRepository localRepository,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<UniversityFullModel> Get(Guid id) {
        var university = await universityRepository.GetRequiredAsync(id);
        var students = await personRepository.ListByUniversityAsync(id);
        var teams = await teamRepository.ListByUniversityAsync(id);
        var local = university.LocalId.HasValue
            ? await localRepository.GetAsync(university.LocalId.Value)
            : null;
        return modelConverter.ToFullModel(university, local, students, teams);
    }

    [HttpPost]
    public async Task<UniversityModel> Create(CreateUniversityModel data) {
        var university = await universityService.AddAsync(data);
        return modelConverter.ToModel(university);
    }
    
    [HttpPut]
    public async Task<UniversityModel> UpdateAsync(UpdateUniversityModel data) {
        var university = await universityService.UpdateAsync(data);
        return modelConverter.ToModel(university);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id) {
        var university = await universityRepository.GetRequiredAsync(id);
        await universityRepository.DeleteAsync(university);
        return NoContent(); // 204 (Ok)
    }
 
    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<UniversityModel>> ListAsync() {
        var universities = await universityRepository.ListAsync();
        return [..universities.ConvertAll(modelConverter.ToModel)];
    }
    
}