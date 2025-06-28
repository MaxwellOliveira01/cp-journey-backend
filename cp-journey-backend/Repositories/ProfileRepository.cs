using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IProfileRepository {

    Task<Profile?> GetAsync(Guid id);
    
    Task<Profile> GetRequiredAsync(Guid id);
    
    Task AddAsync(Profile profile);

    Task DeleteAsync(Profile profile);

    Task<List<Profile>> ListAsync();
    
}

public class ProfileRepository(AppDbContext appDbContext) : IProfileRepository {
    
    public async Task<Profile?> GetAsync(Guid id) {
        const string sql = "SELECT * FROM Profiles WHERE Id = {0}";
        return await appDbContext.Profiles.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public async Task<Profile> GetRequiredAsync(Guid id) {
        var profile = await GetAsync(id);
        if (profile == null) {
            throw new KeyNotFoundException($"Profile with ID {id} not found.");
        }
        return profile;
    }
    
    public async Task AddAsync(Profile entity) {
        const string sql = "INSERT INTO Profiles (Id, Name, Handle, UniversityId) VALUES ({0}, {1}, {2}, {3})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Handle, entity.UniversityId);
    }

    public async Task DeleteAsync(Profile entity) {
        const string sql = "DELETE FROM Profiles WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<List<Profile>> ListAsync() {
        const string sql = "SELECT * FROM Profiles";
        return await appDbContext.Profiles.FromSqlRaw(sql).ToListAsync();
    }

}