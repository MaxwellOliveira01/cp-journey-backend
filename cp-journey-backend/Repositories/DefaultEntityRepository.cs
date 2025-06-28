using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface IDefaultEntityRepository<T> where T : class {
    DbSet<T> Entities { get; }

    Task Add(T entity);

    Task<T?> Get(Guid id);

    Task<T> GetRequired(Guid id);
    
    void Delete(T entity);

    Task SaveChangesAsync();

}

public class DefaultEntityRepository<T>(AppDbContext dbContext) : IDefaultEntityRepository<T> where T : class, IEntity {
    public DbSet<T> Entities => dbContext.Set<T>();

    public async Task Add(T entity)
        => await Entities.AddAsync(entity);

    public async Task<T?> Get(Guid id)
        => await Entities.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<T> GetRequired(Guid id)
    => (await Get(id)) ?? throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
    
    public void Delete(T entity)
        => Entities.Remove(entity);
    
    public async Task SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
}