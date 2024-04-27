using System.Diagnostics;
using System.Reflection;
using SQLite;

namespace gitlist.core;

/// <summary>
/// Создание таблиц в базе данных
/// </summary>
public class DataBase
{
    public void CreateTables(string path, List<Assembly> assemblies)
    {
        using (var connection = GetConnection(path))
        {
            foreach (var types in assemblies.Select(assembly => assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IEntity)) && t.IsClass)).ToList())
            {
                if (!types.Any()) continue;

                try
                {
                    connection.BeginTransaction();

                    foreach (var type in types)
                        connection.CreateTable(type.AsType());

                    connection.Commit();
                }
                catch (Exception)
                {
                    connection.Rollback();
                    throw;
                }
            }
        }
    }

    private SQLiteConnection GetConnection(string path)
    {
        try
        {
            if (!File.Exists(path))
                _ = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

        return new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite);
    }
}