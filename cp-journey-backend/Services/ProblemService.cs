using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IProblemService {
    Task<Problem> AddAsync(ProblemCreateModel data);
    Task<Problem> UpdateAsync(ProblemUpdateModel data);
}

public class ProblemService(
    IProblemRepository problemRepository,
    IPersonRepository personRepository,
    IContestRepository contestRepository
) : IProblemService {
    
    public async Task<Problem> AddAsync(ProblemCreateModel data) {
        var problem = new Problem { Id = Guid.NewGuid() };
        await UpdateFieldsAsync(problem, data);
        await problemRepository.AddAsync(problem);
        return problem;
    }

    public async Task<Problem> UpdateAsync(ProblemUpdateModel data) {
        var problem = await problemRepository.GetRequiredAsync(data.Id);
        await UpdateFieldsAsync(problem, data);
        await problemRepository.UpdateAsync(problem);
        return problem;
    }

    private async Task UpdateFieldsAsync(Problem problem, ProblemCreateModel data) {
        
        var setter = data.SetterId.HasValue 
            ? await personRepository.GetRequiredAsync(data.SetterId.Value) 
            : null;

        var contest = await contestRepository.GetRequiredAsync(data.ContestId);
        
        problem.Name = data.Name;
        problem.Label = data.Label;
        problem.Order = data.Order;
        problem.SetterId = data.SetterId;
        problem.Setter = setter;
        problem.ContestId = data.ContestId;
        problem.Contest = contest;
        // problem.StatementPdf = data.StatementPdf;
    }

}