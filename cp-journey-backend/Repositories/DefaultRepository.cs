namespace cp_journey_backend.Repositories;

public interface IDefaultRepository<T> {
    
    Task<T?> GetAsync(int id);
    
    async Task<T> GetRequiredAsync(int id) 
        => await GetAsync(id) ?? throw new KeyNotFoundException($"Entity {nameof(T)} with ID {id} not found.");
    
    Task AddAsync(T entity);
    
    Task DeleteAsync(T entity);
    
    Task<List<T>> ListAsync();
    
    Task UpdateAsync(T entity);
    
}