using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IContestRepository : IDefaultRepository<Contest> {
    
}

public class ContestRepository(AppDbContext appDbContext) : IContestRepository {
    public async Task<Contest?> GetAsync(Guid id) {
        const string sql = "SELECT * FROM Contests WHERE Id = @Id";
        return await appDbContext.Contests.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public Task AddAsync(Contest entity) {
        const string sql = "INSERT INTO Contests (Id, Name, SiteUrl, StartDate, EndDate, LocalId) " +
                           "VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.SiteUrl, entity.StartDate,
            entity.EndDate, entity.LocalId);
    }

    public Task DeleteAsync(Contest entity) {
        const string sql = "DELETE FROM Contests WHERE Id = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public Task<List<Contest>> ListAsync() {
        const string sql = "SELECT * FROM Contests";
        return appDbContext.Contests.FromSqlRaw(sql).ToListAsync();
    }

    public Task UpdateAsync(Contest entity) {
        const string sql = "UPDATE Contests SET Name = {1}, SiteUrl = {2}, StartDate = {3}, EndDate = {4}, LocalId = {5} " +
                           "WHERE Id = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.SiteUrl, entity.StartDate,
            entity.EndDate, entity.LocalId);
    }
    
}