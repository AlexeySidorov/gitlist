namespace gitlist.core;

/// <summary>
/// ������� ��������� ��������
/// </summary>
public interface IEntity : IEntity<int>
{
}

/// <summary>
/// ������� generic ��������� ��������
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntity<T>
{
    /// <summary>
    ///���������� ������������� ��������
    /// </summary>
    /// 

    T Id { get; set; }
}