using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface ITeamResultsService {
    
    Task<TeamResult> AddAsync(TeamResultUpdateModel data);
    
}

public class TeamResultsService(ITeamResultsRepository teamResultsRepository) : ITeamResultsService {
    
    public async Task<TeamResult> AddAsync(TeamResultUpdateModel data) {
        var teamResult = new TeamResult {
            TeamId = data.TeamId,
            ContestId = data.ContestId,
            Position = data.Position,
            Penalty = data.Penalty
        };
        
        teamResult.Submissions = data.Submissions.Select(s => new Submission {
            TeamResult = teamResult,
            ProblemId = s.ProblemId,
            Accepted = s.Accepted,
            Penalty = s.Penalty,
            Tries = s.Tries,
        }).ToList();

        await teamResultsRepository.AddAsync(teamResult);
        
        return teamResult;
    }

}