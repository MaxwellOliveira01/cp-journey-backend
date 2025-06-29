using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("teams")]
public class TeamController(
    ITeamRepository teamRepository,
    ITeamService teamService,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<TeamModel> GetAsync(Guid id) {
        var team = await teamRepository.GetRequiredAsync(id);
        return modelConverter.ToModel(team);
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
    public async Task<IActionResult> DeleteAsync(Guid id) {
        var team = await teamRepository.GetRequiredAsync(id);
        await teamRepository.DeleteAsync(team);
        return NoContent(); // 204 (Ok)
    }
    
    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<TeamModel>> ListAsync() {
        var teams = await teamRepository.ListAsync();
        return teams.ConvertAll(modelConverter.ToModel);
    }
    
}