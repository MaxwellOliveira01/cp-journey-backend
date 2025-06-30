using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IEventRepository {
    Task<Event?> GetAsync(Guid id);
    
    Task<Event> GetRequiredAsync(Guid id);
    
    Task AddAsync(Event entity);
    
    Task DeleteAsync(Event entity);
    
    Task<List<Event>> ListAsync();
    
    Task UpdateAsync(Event entity);

    Task<List<Event>> ListByUserAsync(Guid userId);
}

public class EventRepository(AppDbContext appDbContext) : IEventRepository {
    
    public async Task AddAsync(Event entity) {
        const string sql = "INSERT INTO Events (Id, Name, Start, End, Description, WebsiteUrl) VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Start, entity.End, entity.Description, entity.WebsiteUrl);
        
        foreach (var participant in entity.Participants) {
            const string memberSql = "INSERT INTO EventParticipations (PersonId, EventId) VALUES ({0}, {1})";
            await appDbContext.Database.ExecuteSqlRawAsync(memberSql, participant.PersonId, participant.EventId);
        }
        
    }

    public Task DeleteAsync(Event entity) {
        const string sql = "DELETE FROM Events WHERE Id = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<Event?> GetAsync(Guid id) {
        const string sql = "SELECT * FROM Events WHERE Id = {0}";
        return await appDbContext.Events.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }

    public async Task<Event> GetRequiredAsync(Guid id)
        => await GetAsync(id) ?? throw new KeyNotFoundException($"Event with ID {id} not found.");

    public async Task UpdateAsync(Event entity) {
        const string sql = "UPDATE Events SET Name = {1}, Start = {2}, End = {3}, Description = {4}, WebsiteUrl = {5} WHERE Id = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.Name, entity.Start, entity.End, entity.Description, entity.WebsiteUrl);
        
        const string deleteParticipantsSql = "DELETE FROM EventParticipations WHERE EventId = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(deleteParticipantsSql, entity.Id);
        
        foreach (var participant in entity.Participants) {
            const string memberSql = "INSERT INTO EventParticipations (PersonId, EventId) VALUES ({0}, {1})";
            await appDbContext.Database.ExecuteSqlRawAsync(memberSql, participant.PersonId, participant.EventId);
        }
        
    }

    public async Task<List<Event>> ListAsync() {
        const string sql = "SELECT * FROM Events";
        return await appDbContext.Events.FromSqlRaw(sql).ToListAsync();
    }
    
    public async Task<List<Event>> ListByUserAsync(Guid userId) {
        const string sql = "SELECT e.* FROM Events e " +
                           "JOIN EventParticipations ep ON e.Id = ep.EventId " +
                           "WHERE ep.PersonId = {0}";
        return await appDbContext.Events.FromSqlRaw(sql, userId).ToListAsync();
    }
    
}