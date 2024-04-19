
using System.Linq.Expressions;
using SQLite;


namespace gitlist.core;

    /// <summary>
    /// Реализация IOrderable через IQueryable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryableOrderable<T> : IOrderable<T> where T : class, new()
    {
        private TableQuery<T> _queryable;

        /// <summary>
        ///
        /// </summary>
        /// <param name="enumerable"></param>
        public QueryableOrderable(TableQuery<T> enumerable)
        {
            _queryable = enumerable;
        }

        /// <summary>
        /// IQueryable
        /// </summary>
        public TableQuery<T> Queryable => _queryable;

        /// <summary>
        /// Сортировать по возрастанию по 1 полю
        /// </summary>
        /// <param name="keySelector"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IOrderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderBy(keySelector);
            return this;
        }

        /// <summary>
        /// Сортировать по возрастанию по 2м полям
        /// </summary>
        /// <param name="keySelector1"></param>
        /// <param name="keySelector2"></param>
        /// <typeparam name="TKey1"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <returns></returns>
        public IOrderable<T> Asc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                              Expression<Func<T, TKey2>> keySelector2)
        {
            _queryable = _queryable
               .OrderBy(keySelector1)
               .ThenBy(keySelector2);
            return this;
        }

        /// <summary>
        /// Сортировать по возрастанию по 3м полям
        /// </summary>
        /// <param name="keySelector1"></param>
        /// <param name="keySelector2"></param>
        /// <param name="keySelector3"></param>
        /// <typeparam name="TKey1"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <typeparam name="TKey3"></typeparam>
        /// <returns></returns>
        public IOrderable<T> Asc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                     Expression<Func<T, TKey2>> keySelector2,
                                                     Expression<Func<T, TKey3>> keySelector3)
        {
            _queryable = _queryable
                .OrderBy(keySelector1)
                .ThenBy(keySelector2)
                .ThenBy(keySelector3);
            return this;
        }

        /// <summary>
        /// Сортировать по убыванию по 1 полю
        /// </summary>
        /// <param name="keySelector"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IOrderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector);
            return this;
        }

        /// <summary>
        /// Сортировать по убыванию по 2м полям
        /// </summary>
        /// <param name="keySelector1"></param>
        /// <param name="keySelector2"></param>
        /// <typeparam name="TKey1"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <returns></returns>
        public IOrderable<T> Desc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                               Expression<Func<T, TKey2>> keySelector2)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector1)
                .ThenByDescending(keySelector2);
            return this;
        }

        /// <summary>
        /// Сортировать по убыванию по 3м полям
        /// </summary>
        /// <param name="keySelector1"></param>
        /// <param name="keySelector2"></param>
        /// <param name="keySelector3"></param>
        /// <typeparam name="TKey1"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <typeparam name="TKey3"></typeparam>
        /// <returns></returns>
        public IOrderable<T> Desc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                      Expression<Func<T, TKey2>> keySelector2,
                                                      Expression<Func<T, TKey3>> keySelector3)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector1)
                .ThenByDescending(keySelector2)
                .ThenByDescending(keySelector3);
            return this;
        }
}