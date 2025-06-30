using cp_journey_backend.Entities;
using cp_journey_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface ITeamRepository  {

    Task<Team?> GetAsync(Guid id);
    
    Task<Team> GetRequiredAsync(Guid id);
    
    Task AddAsync(Team person);

    Task DeleteAsync(Team person);
    
    Task<List<Team>> ListAsync();

    Task UpdateAsync(Team team);

    Task<List<Team>> GetTeamsOfUser(Guid userId);

    Task<List<Team>> GetTeamsOfUniversity(Guid universityId);


}

public class TeamRepository(AppDbContext appDbContext) : ITeamRepository {

    public async Task<Team?> GetAsync(Guid id) {
        const string sql = "SELECT * FROM Teams WHERE Id = {0}";
        return await appDbContext.Teams.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public async Task<Team> GetRequiredAsync(Guid id)
        => await GetAsync(id) ?? throw new KeyNotFoundException($"Team with ID {id} not found."); 

    public async Task AddAsync(Team team) { 
        const string sql = "INSERT INTO Teams (Id, Name, UniversityId) VALUES ({0}, {1}, {2})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, team.Id, team.Name, team.UniversityId);
        
        foreach (var member in team.Members) {
            const string memberSql = "INSERT INTO TeamMembers (TeamId, PersonId) VALUES ({0}, {1})";
            await appDbContext.Database.ExecuteSqlRawAsync(memberSql, member.TeamId, member.PersonId);
        }
    }

    public Task DeleteAsync(Team team) {
        const string sql = "DELETE FROM Teams WHERE Id = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, team.Id);
    }

    public Task<List<Team>> ListAsync() {
        const string sql = "SELECT * FROM Teams";
        return appDbContext.Teams.FromSqlRaw(sql).ToListAsync();
    }

    public async Task UpdateAsync(Team team) {
        const string sql = "UPDATE Teams SET UniversityId = {1} WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, team.Id, team.UniversityId);
        
        const string deleteMembersSql = "DELETE FROM TeamMembers WHERE TeamId = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(deleteMembersSql, team.Id);
        
        foreach (var member in team.Members) {
            const string memberSql = "INSERT INTO TeamMembers (TeamId, PersonId) VALUES ({0}, {1})";
            await appDbContext.Database.ExecuteSqlRawAsync(memberSql, team.Id, member.PersonId);
        }
    }
    
    public async Task<List<Team>> GetTeamsOfUser(Guid userId) {
        const string sql = "SELECT t.* FROM Teams t " +
                           "JOIN TeamMembers tm ON t.Id = tm.TeamId " +
                           "WHERE tm.PersonId = {0}";
        return await appDbContext.Teams.FromSqlRaw(sql, userId).ToListAsync();
    }
    
    public async Task<List<Team>> GetTeamsOfUniversity(Guid universityId) {
        const string sql = "SELECT * FROM Teams WHERE UniversityId = {0}";
        return await appDbContext.Teams.FromSqlRaw(sql, universityId).ToListAsync();
    }
    
}