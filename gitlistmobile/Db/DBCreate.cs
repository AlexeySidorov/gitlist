
using System.Reflection;
using SQLite;
using gitlist.core;

namespace gitlistmobile;

    /// <summary>
    /// Создание таблиц в базе данных
    /// </summary>
    public class DataBase
    {
        public void CreateDataBase(List<Assembly> assemblies)
        {
            using (var connection = ServiceHelper.GetService<IDataBaseService>()?.GetConnection(Constaint.Current.DbName))
            {
                foreach (var types in assemblies.Select(assembly => assembly.DefinedTypes.Where(
                                    t => t.ImplementedInterfaces.Any(i => i == typeof(IEntity)) && t.IsClass)).ToList())
                {
                    // ReSharper disable once PossibleMultipleEnumeration
                    if (!types.Any()) continue;

                    try
                    {
                        if(connection == null) return;

                        connection.BeginTransaction();
                        Migration(connection);

                        // ReSharper disable once PossibleMultipleEnumeration
                        foreach (var type in types)
                            connection.CreateTable(type.AsType(), CreateFlags.AllImplicit);

                        connection.Commit();
                    }
                    catch (Exception)
                    {
                        connection?.Rollback();
                        throw;
                    }
                }
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private void Migration(SQLiteConnection connection)
        {

        }
    }