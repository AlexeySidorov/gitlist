

namespace gitlist.core;

public abstract class AsyncBaseDataService<T> : ICrudServiceAsync<T> where T : class, IEntity
{
    protected readonly IAsyncRepository<T> Repository;

    protected AsyncBaseDataService(IAsyncRepository<T> repository)
    {
        Repository = repository;
    }

    /// <summary>
    /// Загрузить все записи таблицы
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Repository.FetchAsync();
    }

    /// <summary>
    /// Вставить запись
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task CreateAsync(T entity)
    {
        await Repository.CreateAsync(entity);
    }

    /// <summary>
    /// Загрузить все запись по ид
    /// </summary>
    /// <returns></returns>
    public async Task<T> GetAsync(long id)
    {
        return await Repository.GetAsync(id);
    }

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(T entity)
    {
        await Repository.DeleteAsync(entity);
    }

    /// <summary>
    /// Обновить запись
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync(T entity)
    {
        await Repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Очисить таблицу
    /// </summary>
    /// <returns></returns>
    public async Task DeleteAllAsync()
    {
        foreach (var entity in await Repository.FetchAsync())
            await Repository.DeleteAsync(entity);
    }
}