using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface ITeamService {
    Task<Team> AddAsync(CreateTeamModel data);
    Task<Team> UpdateAsync(UpdateTeamModel data);
}

public class TeamService(ITeamRepository teamRepository, ModelConverter modelConverter): ITeamService {
    
    public async Task<Team> AddAsync(CreateTeamModel data) {
        var team = new Team {
            Name = data.Name,
            UniversityId = data.UniversityId
        };
        
        team.Members = (data.MemberIds ?? []).Select(id => new TeamMember {
            PersonId = id,
            TeamId = team.Id
        }).ToList();
        
        await teamRepository.AddAsync(team);
        return team;
    } 
    
    public async Task<Team> UpdateAsync(UpdateTeamModel data) {
        var team = await teamRepository.GetRequiredAsync(data.Id);
        
        team.Name = data.Name;
        team.UniversityId = data.UniversityId;
        
        team.Members = (data.MemberIds ?? []).Select(id => new TeamMember {
            PersonId = id,
            TeamId = team.Id
        }).ToList();
        
        await teamRepository.UpdateAsync(team);
        
        return team;
    }
    
}