using System.Linq.Expressions;

namespace gitlist.core;

public interface IOrderable<T>
{
    IOrderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector);

    IOrderable<T> Asc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                                   Expression<Func<T, TKey2>> keySelector2);

    IOrderable<T> Asc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                          Expression<Func<T, TKey2>> keySelector2,
                                                          Expression<Func<T, TKey3>> keySelector3);

    IOrderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector);

    IOrderable<T> Desc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                                    Expression<Func<T, TKey2>> keySelector2);

    IOrderable<T> Desc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                           Expression<Func<T, TKey2>> keySelector2,
                                                           Expression<Func<T, TKey3>> keySelector3);
}