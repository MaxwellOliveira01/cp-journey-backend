using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/contests")]
public class ContestController(
    IContestRepository contestRepository,
    IContestService contestService,
    ILocalRepository localRepository,
    IProblemRepository problemRepository,
    ITeamResultsRepository teamResultsRepository,
    ISubmissionRepository submissionRepository,
    ModelConverter modelConverter
) : ControllerBase{
    
    [HttpGet("{id}")]
    public async Task<ContestFullModel> Get(int id) {
        var contest = await contestRepository.GetRequiredAsync(id);
        
        var problems = await problemRepository.ListByContestAsync(contest.Id);
        
        var local = contest.LocalId.HasValue
            ? await localRepository.GetAsync(contest.LocalId.Value)
            : null;
        
        var results = await teamResultsRepository.ListByContestAsync(contest.Id);
        var resultsFullModel = new List<TeamResultFullModel>();
        
        foreach (var result in results) {
            var submissions = await submissionRepository.ListByResusltId(result.Id);
            resultsFullModel.Add(modelConverter.ToFullModel(result, submissions));
        }
        
        return modelConverter.ToFullModel(contest, problems, resultsFullModel, local);
    }

    [HttpPost]
    public async Task<ContestModel> Create(ContestCreateModel data) {
        var contest = await contestService.AddAsync(data);
        return modelConverter.ToModel(contest);
    }

    [HttpPut]
    public async Task<ContestModel> UpdateAsync(ContestUpdateModel data) {
        var contest = await contestService.UpdateAsync(data);
        return modelConverter.ToModel(contest);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var contest = await contestRepository.GetRequiredAsync(id);
        await contestRepository.DeleteAsync(contest);
        return NoContent();
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<ContestModel>> ListAsync() {
        var contests = await contestRepository.ListAsync();
        return [..contests.ConvertAll(modelConverter.ToModel)];
    }
    
    [HttpGet("filter")] // TODO: implement pagination
    public async Task<List<ContestModel>> ListAsync(string? pref) {
        var contests = await contestRepository.FilterAsync(pref);
        return [..contests.ConvertAll(modelConverter.ToModel)];
    }
    
    [HttpGet("{id}/statements")]
    public async Task<IActionResult> DonwloadStatementAsync(int id) {
        var contest = await contestRepository.GetRequiredAsync(id);
        if (contest.StatementsPdf == null) {
            return NotFound("Contest statement not found.");
        }
        return File(contest.StatementsPdf, "application/pdf", $"{contest.Name}_statements.pdf");
    }
    
    [HttpGet("{id}/tutorial")]
    public async Task<IActionResult> DownloadTutorialAsync(int id) {
        var contest = await contestRepository.GetRequiredAsync(id);
        if (contest.TutorialPdf == null) {
            return NotFound("Contest statement not found.");
        }
        return File(contest.TutorialPdf, "application/pdf", $"{contest.Name}_tutorial.pdf");
    }
    
}