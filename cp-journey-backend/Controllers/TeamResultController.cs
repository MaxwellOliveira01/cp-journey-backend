using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/team-results")]
public class TeamResultController(
    ITeamResultsRepository teamResultsRepository,
    ISubmissionRepository submissionRepository,
    ITeamResultsService teamResultsService,
    ModelConverter modelConverter
) : ControllerBase {

    [HttpGet("exists")]
    public async Task<ResultExistsModel> ExistsAsync(int teamId, int contestId) {
        var exists = await teamResultsRepository.ExistsAsync(teamId, contestId);
        return new ResultExistsModel {
            Exists = exists
        };
    }

    [HttpGet("by-team-and-contest")]
    public async Task<TeamResultModel> GetByTeamAndContestAsync(int teamId, int contestId) {
        var result = await teamResultsRepository.GetByTeamAndContestAsync(teamId, contestId);        
        var submissions = await submissionRepository.GetByResusltId(result.Id);
        return modelConverter.ToFullModel(result, submissions);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(TeamResultUpdateModel data) {
        var _ = await teamResultsService.AddAsync(data);
        return Ok();
    }
    
}