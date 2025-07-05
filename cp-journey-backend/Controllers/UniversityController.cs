using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/universities")]
public class UniversityController(
    IUniversityRepository universityRepository,
    IUniversityService universityService,
    IPersonRepository personRepository,
    ITeamRepository teamRepository,
    ILocalRepository localRepository,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<UniversityFullModel> Get(int id) {
        var university = await universityRepository.GetRequiredAsync(id);
        var students = await personRepository.ListByUniversityAsync(id);
        var teams = await teamRepository.ListByUniversityAsync(id);
        var local = university.LocalId.HasValue
            ? await localRepository.GetAsync(university.LocalId.Value)
            : null;
        return modelConverter.ToFullModel(university, local, students, teams);
    }

    [HttpPost]
    public async Task<UniversityModel> Create(UniversityCreateModel data) {
        var university = await universityService.AddAsync(data);
        return modelConverter.ToModel(university);
    }
    
    [HttpPut]
    public async Task<UniversityModel> UpdateAsync(UniversityUpdateModel data) {
        var university = await universityService.UpdateAsync(data);
        return modelConverter.ToModel(university);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var university = await universityRepository.GetRequiredAsync(id);
        await universityRepository.DeleteAsync(university);
        return NoContent(); // 204 (Ok)
    }
 
    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<UniversityModel>> ListAsync() {
        var universities = await universityRepository.ListAsync();
        return [..universities.ConvertAll(modelConverter.ToModel)];
    }
    
    [HttpGet("list/search-model")]
    public async Task<List<UniversitySearchModel>> ListSearchModelAsync() {
        var universities = await universityRepository.ListAsync();

        var tasks = universities.Select(async s => {
            var local = s.LocalId.HasValue
                ? await localRepository.GetAsync(s.LocalId.Value)
                : null;

            return modelConverter.ToSearchModel(s, local);
        });

        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }
    
}