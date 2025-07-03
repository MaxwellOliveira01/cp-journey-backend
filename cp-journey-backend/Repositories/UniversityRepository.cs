using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IUniversityRepository : IDefaultRepository<University> {
    
}

public class UniversityRepository(AppDbContext appDbContext) : IUniversityRepository {
    
    public async Task AddAsync(University entity) {
        var sql = "INSERT INTO Universities (Id, Name, Alias, LocalId) VALUES ({0}, {1}, {2}, {3})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Alias, entity.LocalId);
    }

    public Task DeleteAsync(University entity) {
        var sql = "DELETE FROM Universities WHERE Id = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<University?> GetAsync(Guid id) {
        var sql = "SELECT * FROM Universities WHERE Id = {0}";
        return await appDbContext.Universities.FromSqlRaw(sql, id).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<University> GetRequiredAsync(Guid id) {
        var university = await GetAsync(id);
        if (university == null) {
            throw new KeyNotFoundException($"University with ID {id} not found.");
        }
        return university;
    }
    
    public async Task UpdateAsync(University entity) {
        const string sql = "UPDATE Universities SET Name = {1}, Alias = {2}, LocalId = {3} WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Alias, entity.LocalId);
    }
    
    public async Task<List<University>> ListAsync() {
        const string sql = "SELECT * FROM Universities";
        return await appDbContext.Universities.FromSqlRaw(sql).ToListAsync();
    }

}