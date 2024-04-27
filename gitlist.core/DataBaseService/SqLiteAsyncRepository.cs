using SQLite;
using System.Linq.Expressions;

namespace gitlist.core.DataBaseService
{
    public class SqLiteAsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly string _path;
        private SQLiteConnection _connection;

        public SQLiteConnection Connection
        {
            get { return _connection ?? (_connection = new SQLiteConnection(_path, SQLiteOpenFlags.ReadWrite)); }
        }

        private readonly AsyncLock _lock = new AsyncLock();

        public SqLiteAsyncRepository()
        {
            _path = ServiceContainer.Current.DbPath;
        }

        public void Dispose()
        {

            if (Connection != null)
                Connection.Dispose();
            _connection = null;
        }

        private void Current_CloseConnectionDb(object sender, EventArgs e)
        {
            Disconect();
        }

        public void Disconect()
        {
            Dispose();
        }

        /// <summary> using (_lock.Lock())
        /// Вставка одной строки в таблицу
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    Connection.Insert(entity);
                    return entity;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Вставка нескольких строк  в таблицу
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task CreateAllAsync(IEnumerable<TEntity> entities)
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    Connection.InsertAll(entities);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Обновление строки в таблице
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    Connection.Update(entity);
                    return entity;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Обновление не скольких строк
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task UpdateAllAsync(IEnumerable<TEntity> entities)
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    Connection.UpdateAll(entities);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// удаление строки из таблицы
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    Connection.Delete(entity);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Удаление нескольких записей скольких записей
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task DeleteAllAsync(IEnumerable<TEntity> entities)
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    foreach (var entity in entities)
                        Connection.Delete(entity);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Удаление всех записей скольких записей
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                    Connection.DeleteAll<TEntity>();
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Выборка строки по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(long id)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().FirstOrDefault(i => i.Id == id);
                    return result;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Выборка одной строки, по условию
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().FirstOrDefault(predicate.Compile());
                    return result;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Количество записей в таблице
        /// </summary>
        /// <returns></returns>
        public async Task<long> CountAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().Count();
                    return (long)result;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Количество записей в таблице удовлетворяющие условию
        /// </summary>
        /// <returns></returns>
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().Count(predicate);
                    return (long)result;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить все записи
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().ToList();
                    return result;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>wd
        /// Загрузить все записи удовлетворяющие условию
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().Where(predicate);
                    return result.ToList();
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить все записи и отсортировать
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync(Action<IOrderable<TEntity>> order)
        {
            try
            {
                return await Task.Factory.StartNew(() =>
                {
                    using (_lock.Lock())
                    {
                        var list = Connection.Table<TEntity>();
                        return Query(order, list);
                    }
                }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return new List<TEntity>();
            }

        }

        /// <summary>
        /// Загрузить записи удовлетворяющие условию и отсортировать
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, Action<IOrderable<TEntity>> order)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var list = Connection.Table<TEntity>().Where(predicate);
                    return Query(order, list);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить count записей начиная с skip записи и отсортировать
        /// </summary>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> FetchAsync(Action<IOrderable<TEntity>> order, int skip, int count)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var list = Connection.Table<TEntity>().Skip(skip).Take(count);
                    return Query(order, list);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить count записей начиная с skip записи удовлетворяющих условию
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, int skip, int count)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var result = Connection.Table<TEntity>().Where(predicate).Skip(skip).Take(count);
                    return result;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить count записей начиная с skip записи удовлетворяющих условию и отсортировать
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, Action<IOrderable<TEntity>> order, int skip, int count)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var list = Connection.Table<TEntity>().Where(predicate).Skip(skip).Take(count);
                    return Query(order, list).ToList();
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить count записей удовлетворяющих условию и отсортировать
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, Action<IOrderable<TEntity>> order, int count)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var list = Connection.Table<TEntity>().Where(predicate).Take(count);
                    return Query(order, list);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Загрузить count записей удовлетворяющих условию и отсортировать
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FetchAsync(List<TEntity> array, Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    var list = Connection.Table<TEntity>().Where(predicate).Union(array);
                    return list;
                }
            }).ConfigureAwait(false);
        }

        private static IList<TEntity> Query(Action<IOrderable<TEntity>> order, TableQuery<TEntity> T)
        {
            var orderable = new QueryableOrderable<TEntity>(T);
            order(orderable);
            return orderable.Queryable.AsEnumerable().ToList();
        }

        /// <summary>
        /// Создать или обновить элемент
        /// </summary>
        /// <param name="entity">Элемент</param>
        /// <returns></returns>
        public async Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                {
                    Connection.Insert(entity, "OR REPLACE");
                    return entity;
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Создать или обновить элементы
        /// </summary>
        /// <param name="entities">Элементы</param>
        /// <returns></returns>
        public async Task CreateOrUpdateAsync(IList<TEntity> entities)
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                    //  var ids = entities.Select(e => e.Id).ToArray();
                    // Connection.DeleteAll<TEntity>(ids.Cast<object>().ToArray());
                    // Connection.InsertAll(entities);
                    //  Connection.UpdateAll(entities);
                    Connection.InsertAll(entities, "OR REPLACE");
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Удалить элементы по ИД
        /// </summary>
        /// <param name="ids">Список ИД</param>
        /// <returns></returns>
        public async Task DeleteAsync(List<long> ids)
        {
            await Task.Factory.StartNew(() =>
            {
                using (_lock.Lock())
                    Connection.DeleteAll<TEntity>();  //.DeleteAll<TEntity>(ids);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Удалить элементы по условию
        /// </summary>
        /// <param name="predicate">Условие</param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await Task.Factory.StartNew(() =>
            {
                //using (_lock.Lock())
                // Connection.Table<TEntity>().Delete< .Delete<TEntity>(predicate);
            }).ConfigureAwait(false);
        }

        public Task<IList<TEntity>> FetchAsync(int skip, int count)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrUpdateAllAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
