using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamController(
    ITeamRepository teamRepository,
    ITeamService teamService,
    IPersonRepository personRepository,
    IUniversityRepository universityRepository,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<TeamFullModel> GetAsync(int id) {
        var team = await teamRepository.GetRequiredAsync(id);
        var members = await personRepository.ListByTeamAsync(id);
        var university = team.UniversityId.HasValue ? await universityRepository.GetAsync(team.UniversityId.Value) : null;
        return modelConverter.ToFullModel(team, university, members);
    }
    
    [HttpPost]
    public async Task<TeamModel> CreateAsync(CreateTeamModel data) {
        var team = await teamService.AddAsync(data);
        return modelConverter.ToModel(team);
    }
    
    [HttpPut]
    public async Task<TeamModel> UpdateAsync(UpdateTeamModel data) {
        var team = await teamService.UpdateAsync(data);
        return modelConverter.ToModel(team);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var team = await teamRepository.GetRequiredAsync(id);
        await teamRepository.DeleteAsync(team);
        return NoContent(); // 204 (Ok)
    }
    
    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<TeamModel>> ListAsync() {
        var teams = await teamRepository.ListAsync();
        return teams.ConvertAll(modelConverter.ToModel);
    }
    
    [HttpGet("list/search-model")] // TODO: implement pagination
    public async Task<List<TeamSearchModel>> ListSearchAsync() {
        var teams = await teamRepository.ListAsync();
        
        var tasks = teams.Select(async t => {
            var university = t.UniversityId.HasValue
                ? await universityRepository.GetAsync(t.UniversityId.Value)
                : null;

            return modelConverter.ToSearchModel(t, university);
        });
        
        var results = await Task.WhenAll(tasks);
        return results.ToList();;
    }
    
}