using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IProfileRepository {

    Task Add(Profile profile);

    Task Delete(Profile profile);
    
    Task<Profile?> Get(Guid id);
    
    Task<Profile> GetRequired(Guid id);

}

public class ProfileRepository(AppDbContext appDbContext) : IProfileRepository {

    public async Task Add(Profile entity) {
        const string sql = "INSERT INTO Profiles (Id, Name, Handle, UniversityId) VALUES ({0}, {1}, {2}, {3})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Handle, entity.UniversityId);
    }

    public async Task Delete(Profile entity) {
        const string sql = "DELETE FROM Profiles WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<Profile?> Get(Guid id) {
        const string sql = "SELECT * FROM Profiles WHERE Id = {0}";
        return await appDbContext.Profiles.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public async Task<Profile> GetRequired(Guid id) {
        var profile = await Get(id);
        if (profile == null) {
            throw new KeyNotFoundException($"Profile with ID {id} not found.");
        }
        return profile;
    }

}