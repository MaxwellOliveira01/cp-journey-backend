using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/problems")]
public class ProblemController(
    IProblemRepository problemRepository, 
    IProblemService problemService, 
    IPersonRepository personRepository,
    IContestRepository contestRepository,
    ModelConverter modelConverter 
): ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<ProblemFullModel> Get(int id) {
        var problem = await problemRepository.GetRequiredAsync(id);
        var setter = problem.SetterId.HasValue 
            ? await personRepository.GetRequiredAsync(problem.SetterId.Value) 
            : null;
        var contest = await contestRepository.GetRequiredAsync(problem.ContestId);
        return modelConverter.ToFullModel(problem, setter, contest);
    }
    
    [HttpPost]
    public async Task<ProblemModel> Create(ProblemCreateModel data) {
        var problem = await problemService.AddAsync(data);
        return modelConverter.ToModel(problem);
    }

    [HttpPut]
    public async Task<ProblemModel> UpdateAsync(ProblemUpdateModel data) {
        var problem = await problemService.UpdateAsync(data);
        return modelConverter.ToModel(problem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var problem = await problemRepository.GetRequiredAsync(id);
        await problemRepository.DeleteAsync(problem);
        return NoContent();
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<ProblemModel>> ListAsync() {
        var problems = await problemRepository.ListAsync();
        var orderedProblems = problems.OrderBy(p => p.ContestId).ThenBy(p => p.Order).ToList();
        return [..orderedProblems.ConvertAll(modelConverter.ToModel)];
    }
    
}