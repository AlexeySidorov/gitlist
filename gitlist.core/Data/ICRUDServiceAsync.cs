
namespace gitlist.core;

public interface ICrudServiceAsync<T> : IDependency where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task<T> GetAsync(long id);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAllAsync();
}
