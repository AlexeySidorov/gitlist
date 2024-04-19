using System.Linq.Expressions;

namespace gitlist.core;

/// <summary>
/// ����������� ����������� ��� ������ � ��
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAsyncRepository<TEntity> : IDependency, IDisposable where TEntity : class, IEntity
{
    /// <summary>
    /// ������� ������
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    /// �������� ������� �������
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task CreateAllAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// ������� ������
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// ��������� ������ �� Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(long id);

    /// <summary>
    /// ��������� ������ �� �������
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// ���������� ���� �������
    /// </summary>
    /// <returns></returns>
    Task<long> CountAsync();

    /// <summary>
    /// ���������� ������� ��������������� �������
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// ��������� ��� ������
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FetchAsync();

    /// <summary>
    /// ��������� ������ �� �������
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<IList<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// ��������� ��� ������ � �������������
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FetchAsync(Action<IOrderable<TEntity>> order);

    /// <summary>
    /// ��������� ������ �� ������� � �������������
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, Action<IOrderable<TEntity>> order);

    /// <summary>
    /// ��������� count ������� ������� � skip ������ 
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    Task<IList<TEntity>> FetchAsync(int skip, int count);

    /// <summary>
    /// ��������� count ������� ������� � skip ������ � �������������
    /// </summary>
    /// <param name="order"></param>
    /// <param name="skip"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    Task<IList<TEntity>> FetchAsync(Action<IOrderable<TEntity>> order, int skip, int count);

    /// <summary>
    /// ��������� count ������� ������� � skip ������ ��������������� �������
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="skip"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, int skip, int count);

    /// <summary>
    /// ��������� count ������� ������� � skip ������ ��������������� ������� � �������������
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="order"></param>
    /// <param name="skip"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, Action<IOrderable<TEntity>> order, int skip, int count);


    /// <summary>
    /// ��������� count ������� ��������������� ������� � �������������
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="order"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, Action<IOrderable<TEntity>> order, int count);

    /// <summary>
    ///  Join 2� ������
    /// </summary>
    /// <param name="sourceColumn"></param>
    /// <param name="destinationColumn"></param>
    /// <param name="sourceTableColumnSelection"></param>
    /// <param name="destinationTableColumnSelection"></param>
    /// <param name="sourceWhere"></param>
    /// <param name="destinationWhere"></param>
    /// <param name="skip"></param>
    /// <param name="count"></param>
    /// <typeparam name="TSourceTable"></typeparam>
    /// <typeparam name="TDestinationTable"></typeparam>
    /// <returns></returns>
    /// <summary>
    /// ��������� ������ � ��
    /// </summary>
    /// <param name="sqlQuery"></param>
    /// <param name="array"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <summary>
    /// ������ ����������
    /// </summary>
    /// <returns></returns>
    //  Task BeginTransaction(IEnumerable<TEntity> T);

    // Task<IEnumerable<TEntity>> FetchAsync(List<TEntity> array, Expression<Func<TEntity, bool>> predicate);

    Task UpdateAllAsync(IEnumerable<TEntity> entities);
    /// <summary>
    /// ������� ��� �������� �������
    /// </summary>
    /// <param name="entity">�������</param>
    /// <returns></returns>
    Task<TEntity> CreateOrUpdateAsync(TEntity entity);
    /// <summary>
    /// ������� ��� �������� ��������
    /// </summary>
    /// <param name="entities">��������</param>
    /// <returns></returns>
    Task CreateOrUpdateAllAsync(IEnumerable<TEntity> entities);
    /// <summary>
    /// ������� �������� �� �������
    /// </summary>
    /// <param name="predicate">�������</param>
    /// <returns></returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
}