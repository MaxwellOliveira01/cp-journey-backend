using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IProblemRepository : IDefaultRepository<Problem> {
    
    Task<List<Problem>> ListByContestAsync(Guid contestId);
    
    Task<List<Problem>> ListBySetterAsync(Guid setterId);
    
}

public class ProblemRepository(AppDbContext appDbContext) : IProblemRepository {
    
    public async Task<Problem?> GetAsync(Guid id) {
        const string sql = "SELECT * FROM Problems WHERE Id = {0}";
        return await appDbContext.Problems.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public Task AddAsync(Problem entity) {
        const string sql = "INSERT INTO Problems (Id, Name, Label, Order, ContestId, SetterId) " +
                           "VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
        
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Label, entity.Order,
            entity.ContestId, entity.SetterId);
    }

    public Task DeleteAsync(Problem entity) {
        const string sql = "DELETE FROM Problems WHERE Id = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public Task<List<Problem>> ListAsync() {
        const string sql = "SELECT * FROM Problems";
        return appDbContext.Problems.FromSqlRaw(sql).ToListAsync();
    }

    public Task UpdateAsync(Problem entity) {
        const string sql = "UPDATE Problems SET Name = {1}, Label = {2}, Order = {3}, " +
                           "ContestId = {4}, SetterId = {5} WHERE Id = {0}";
        
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Label,
            entity.Order, entity.ContestId, entity.SetterId);
    }

    public Task<List<Problem>> ListByContestAsync(Guid contestId) {
        const string sql = "SELECT * FROM Problems WHERE ContestId = {0}";
        return appDbContext.Problems.FromSqlRaw(sql, contestId).ToListAsync();
    }

    public Task<List<Problem>> ListBySetterAsync(Guid setterId) {
        const string sql = "SELECT * FROM Problems WHERE SetterId = {0}";
        return appDbContext.Problems.FromSqlRaw(sql, setterId).ToListAsync();
    }
}