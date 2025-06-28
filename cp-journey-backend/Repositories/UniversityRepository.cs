using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IUniversityRepository {
    Task AddAsync(University entity);
    void DeleteAsync(University entity);
    Task<University?> GetAsync(Guid id);
    Task<University> GetAsyncRequired(Guid id);
}

public class UniversityRepository(AppDbContext appDbContext) : IUniversityRepository {
    public async Task AddAsync(University entity) {
        var sql = "INSERT INTO Universities (Id, Name, Alias) VALUES ({0}, {1}, {2})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Alias);
    }

    public void DeleteAsync(University entity) {
        var sql = "DELETE FROM Universities WHERE Id = {0}";
        appDbContext.Database.ExecuteSqlRaw(sql, entity.Id);
    }

    public async Task<University?> GetAsync(Guid id) {
        var sql = "SELECT * FROM Universities WHERE Id = {0}";
        return await appDbContext.Universities.FromSqlRaw(sql, id).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<University> GetAsyncRequired(Guid id) {
        var university = await GetAsync(id);
        if (university == null) {
            throw new KeyNotFoundException($"University with ID {id} not found.");
        }
        return university;
    }

}