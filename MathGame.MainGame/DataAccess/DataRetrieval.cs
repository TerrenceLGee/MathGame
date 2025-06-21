using MathGame.MainGame.Options;
using Microsoft.Data.Sqlite;

namespace MathGame.MainGame.DataAccess;
public class DataRetrieval
{
    public static List<Player> RetrievePlayers()
    {
        List<Player> players = new List<Player>();
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseInitializer.ConnectionString))
            {
                connection.Open();
                SqliteCommand retrievalCommand = connection.CreateCommand();

                retrievalCommand.CommandText = @"SELECT * FROM players";

                try
                {
                    using (SqliteDataReader dataReader = retrievalCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                try
                                {
                                    players.Add(
                                        new Player(
                                            dataReader.GetString(1),
                                            (GameOption)Enum.Parse(typeof(GameOption),dataReader.GetString(2)),
                                            (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), dataReader.GetString(3)),
                                            dataReader.GetInt32(4),
                                            dataReader.GetInt32(5),
                                            dataReader.GetDouble(6),
                                            dataReader.GetString(7)
                                            ));
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine($"{ex.Message}");
                                }
                            }
                        }
                    }
                }
                catch (SqliteException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

        return players;
    }
}
