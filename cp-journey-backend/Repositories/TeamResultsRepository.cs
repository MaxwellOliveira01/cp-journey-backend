using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface ITeamResultsRepository {

    Task<TeamResult?> GetAsync(int teamId, int contestId);
    
    Task DeleteAsync(TeamResult entity);
    
    Task AddAsync(TeamResult entity);
    
    Task<bool> ExistsAsync(int teamId, int contestId);

    Task<TeamResult> GetByTeamAndContestAsync(int teamId, int contestId);

}

public class TeamResultsRepository(AppDbContext appDbContext) : ITeamResultsRepository {
    
    public async Task<TeamResult?> GetAsync(int teamId, int contestId) {
        const string sql = "SELECT * FROM \"TeamResults\" WHERE \"TeamId\" = {0} AND \"ContestId\" = {1}";
        return await appDbContext.TeamResults
            .FromSqlRaw(sql, teamId, contestId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(TeamResult entity) {
        await DeleteAsync(entity);
       
        await appDbContext.TeamResults.AddAsync(entity);
        await appDbContext.SaveChangesAsync();
        
        // const string sql = "INSERT INTO \"TeamResults\" (\"TeamId\", \"ContestId\", \"Position\", \"Penalty\") " +
        //                    "VALUES ({0}, {1}, {2}, {3})";
        //
        // await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.TeamId, entity.ContestId, entity.Position, entity.Penalty);
        //
        // const string submissionSql = "INSERT INTO \"Submissions\" (\"TeamResultId\", \"ProblemId\", \"Accepted\", \"Tries\", \"Penalty\") " +
        //                           "VALUES ({0}, {1}, {2}, {3}, {4})";
        //
        // foreach (var submission in entity.Submissions) {
        //     await appDbContext.Database.ExecuteSqlRawAsync(submissionSql, entity.Id, submission.ProblemId, submission.Accepted, submission.Tries, submission.Penalty);
        // }
        
    }

    public Task DeleteAsync(TeamResult entity) {
        const string sql = "DELETE FROM \"TeamResults\" WHERE \"TeamId\" = {0} AND \"ContestId\" = {1}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.TeamId, entity.ContestId);
    }

    public Task<List<TeamResult>> ListAsync() {
        const string sql = "SELECT * FROM \"TeamResults\"";
        return appDbContext.TeamResults.FromSqlRaw(sql).ToListAsync();
    }

    public Task UpdateAsync(TeamResult entity) {
        const string sql = "UPDATE \"TeamResults\" SET \"TeamId\" = {1}, \"ContestId\" = {2}, \"Position\" = {3}, \"Penalty\" = {4} WHERE \"Id\" = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.TeamId, entity.ContestId, entity.Position, entity.Penalty);
    }
    
    public async Task<bool> ExistsAsync(int teamId, int contestId) {
        return await appDbContext.TeamResults        
            .AnyAsync(tr => tr.TeamId == teamId && tr.ContestId == contestId);
    }
    
    public async Task<TeamResult> GetByTeamAndContestAsync(int teamId, int contestId) {
        const string sql = "SELECT * FROM \"TeamResults\" WHERE \"TeamId\" = {0} AND \"ContestId\" = {1}";
        return await appDbContext.TeamResults
            .FromSqlRaw(sql, teamId, contestId)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"TeamResult for TeamId {teamId} and ContestId {contestId} not found.");
    }
    
    
}