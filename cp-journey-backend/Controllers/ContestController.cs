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
    ModelConverter modelConverter
) : ControllerBase{
    
    [HttpGet("{id}")]
    public async Task<ContestModel> Get(Guid id) {
        var contest = await contestRepository.GetRequiredAsync(id);
        return modelConverter.ToModel(contest);
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
    public async Task<IActionResult> DeleteAsync(Guid id) {
        var contest = await contestRepository.GetRequiredAsync(id);
        await contestRepository.DeleteAsync(contest);
        return NoContent();
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<ContestModel>> ListAsync() {
        var contests = await contestRepository.ListAsync();
        return [..contests.ConvertAll(modelConverter.ToModel)];
    }
    
}