using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IEventRepository : IDefaultRepository<Event> {
    Task<List<Event>> ListByUserAsync(int id);
    Task<List<Event>> FilterAsync(string? prefix);
}

public class EventRepository(AppDbContext appDbContext) : IEventRepository {
    
    public async Task AddAsync(Event entity) {
        
        // The entity id is generated by the database
        // And when using rawsql to insert this entry, we dont know the id yet
        // and the query doesnt return it!
        // So we need to use the EF core to handle that
        // EF Core will automatically insert the new entity and put the new id on entity.Id
        
        var participants = entity.Participants;
        entity.Participants = [];
        
        await appDbContext.Events.AddAsync(entity);
        await appDbContext.SaveChangesAsync();

        foreach (var participant in participants) {
            participant.EventId = entity.Id;
        }
        
        await appDbContext.AddRangeAsync(entity.Participants);
        await appDbContext.SaveChangesAsync();
        
    }

    public Task DeleteAsync(Event entity) {
        const string sql = "DELETE FROM \"Events\" WHERE \"Id\" = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<Event?> GetAsync(int id) {
        const string sql = "SELECT * FROM \"Events\" WHERE \"Id\" = {0}";
        return await appDbContext.Events.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Event entity) {
        const string sql = "UPDATE \"Events\" SET \"Name\" = {1}, \"Start\" = {2}, \"End\" = {3}," +
                           "\"Description\" = {4}, \"WebsiteUrl\" = {5}, \"LocalId\" = {6} WHERE \"Id\" = {0}";
        
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Start, entity.End, 
            entity.Description, entity.WebsiteUrl, entity.LocalId);
        
        const string deleteParticipantsSql = "DELETE FROM \"EventParticipations\" WHERE \"EventId\" = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(deleteParticipantsSql, entity.Id);
        
        foreach (var participant in entity.Participants) {
            participant.EventId = entity.Id;
            const string memberSql = "INSERT INTO \"EventParticipations\" (\"PersonId\", \"EventId\") VALUES ({0}, {1})";
            await appDbContext.Database.ExecuteSqlRawAsync(memberSql, participant.PersonId, participant.EventId);
        }
        
    }

    public async Task<List<Event>> ListAsync() {
        const string sql = "SELECT * FROM \"Events\"";
        return await appDbContext.Events.FromSqlRaw(sql).ToListAsync();
    }
    
    public async Task<List<Event>> ListByUserAsync(int userId) {
        const string sql = "SELECT e.* FROM \"Events\" e " +
                           "JOIN \"EventParticipations\" ep ON e.\"Id\" = ep.\"EventId\" " +
                           "WHERE ep.\"PersonId\" = {0}";
        
        return await appDbContext.Events.FromSqlRaw(sql, userId).ToListAsync();
    }
    
    public async Task<List<Event>> FilterAsync(string? prefix) {
        if (string.IsNullOrEmpty(prefix)) {
            return await ListAsync();
        }
        
        const string sql = "SELECT * FROM \"Events\" WHERE LOWER(\"Name\") LIKE LOWER({0})";
        return await appDbContext.Events.FromSqlRaw(sql, $"%{prefix}%").ToListAsync();
    }
    
}