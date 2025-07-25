using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface ILocalRepository : IDefaultRepository<Local> {

}

public class LocalRepository(AppDbContext appDbContext) : ILocalRepository {
    
    public async Task AddAsync(Local entity) {
        // The entity id is generated by the database
        // And when using rawsql to insert this entry, we dont know the id yet
        // and the query doesnt return it!
        // So we need to use the EF core to handle that
        // EF Core will automatically insert the new entity and put the new id on entity.Id
        await appDbContext.Locals.AddAsync(entity);
        await appDbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Local entity) {
        const string sql = "DELETE FROM \"Locals\" WHERE \"Id\" = {0}";
        return appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id);
    }

    public async Task<Local?> GetAsync(int id) {
        const string sql = "SELECT * FROM \"Locals\" WHERE \"Id\" = {0}";
        return await appDbContext.Locals.FromSqlRaw(sql, id).FirstOrDefaultAsync();
    }
    
    public async Task UpdateAsync(Local entity) {
        const string sql = "UPDATE \"Locals\" SET \"City\" = {1}, \"State\" = {2}, \"Country\" = {3} WHERE \"Id\" = {0}";
        await appDbContext.Database.ExecuteSqlRawAsync(sql, entity.Id, entity.City, entity.State, entity.Country);
    }

    public async Task<List<Local>> ListAsync() {
        const string sql = "SELECT * FROM \"Locals\"";
        return await appDbContext.Locals.FromSqlRaw(sql).ToListAsync();
    }
}