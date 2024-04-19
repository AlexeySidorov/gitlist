using SQLite;

namespace gitlistmobile;

public interface IDataBaseService
{
    /// <summary>
    /// Подключение с базой данных
    /// </summary>
    SQLiteConnection GetConnection(string dbName);
}


public class DataBaseService : IDataBaseService
{
    /// <summary>
    /// Подключение с базой данных
    /// </summary>
    public SQLiteConnection GetConnection(string dbName)
    {
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var path = Path.Combine(documentsPath, dbName);

        if (!File.Exists(path))

        try {
            // ReSharper disable once ObjectCreationAsStatement
            _ = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        } catch(Exception e) {
            var d = e;
        }

        return new SQLiteConnection(path);
    }
}