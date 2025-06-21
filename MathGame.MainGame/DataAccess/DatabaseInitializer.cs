using Microsoft.Data.Sqlite;

namespace MathGame.MainGame.DataAccess;
public class DatabaseInitializer
{
    public static string? ConnectionString { get; set; }
    public DatabaseInitializer(string? connectionString)
    {
        ConnectionString = connectionString;
        CreateDatabase();
    }

    private void CreateDatabase()
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand tableCommand = connection.CreateCommand())
                {
                    tableCommand.CommandText =
                        @"CREATE TABLE IF NOT EXISTS players (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        Name Text,
                        Operation Text,
                        Difficulty Text,
                        Correct Integer,
                        Wrong Integer,
                        Percentage Real,
                        Elapsed Text);";

                    tableCommand.ExecuteNonQuery();
                }
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}
