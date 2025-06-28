using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IPersonRepository {

    Task<Person?> GetAsync(Guid id);
    
    Task<Person> GetRequiredAsync(Guid id);
    
    Task AddAsync(Person person);

    Task DeleteAsync(Person person);
    
    Task<List<Person>> ListAsync();

    Task UpdateAsync(Person person);

}

public class PersonRepository(AppDbContext appDbContext) : IPersonRepository {
    
    public async Task<Person?> GetAsync(Guid id) {
        const string sql = "SELECT * FROM Profiles WHERE Id = {0}";
        return await appDbContext.Persons.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public async Task<Person> GetRequiredAsync(Guid id) {
        var profile = await GetAsync(id);
        if (profile == null) {
            throw new KeyNotFoundException($"Person with ID {id} not found.");
        }
        return profile;
    }
    
    public async Task AddAsync(Person entity) {
        const string sql = "INSERT INTO Persons (Id, Name, Handle, UniversityId) VALUES ({0}, {1}, {2}, {3})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Handle, entity.UniversityId);
    }

    public async Task DeleteAsync(Person entity) {
        const string sql = "DELETE FROM Persons WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<List<Person>> ListAsync() {
        const string sql = "SELECT * FROM Persons";
        return await appDbContext.Persons.FromSqlRaw(sql).ToListAsync();
    }
    
    public async Task UpdateAsync(Person entity) {
        const string sql = "UPDATE Profiles SET Name = {1}, Handle = {2}, UniversityId = {3} WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Handle, entity.UniversityId);
    }

}