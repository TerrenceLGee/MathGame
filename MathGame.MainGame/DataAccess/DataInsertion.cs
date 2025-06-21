using Microsoft.Data.Sqlite;

namespace MathGame.MainGame.DataAccess;
public class DataInsertion
{
    public static bool InsertPlayer(Player player)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(DatabaseInitializer.ConnectionString))
            {
                connection.Open();
                SqliteCommand insertCommand = connection.CreateCommand();
                string name = player.Name!;
                string operation = player.GameOperation.ToString();
                string difficulty = player.Level.ToString();
                int correct = player.NumberCorrect;
                int wrong = player.NumberWrong;
                double percentage = player.PercentageCorrect;
                string elapsed = player.ElapsedTime!;

                insertCommand.CommandText = $"INSERT INTO players (Name, Operation, Difficulty, Correct, Wrong, Percentage, Elapsed) VALUES(@Name, @Operation, @Difficulty, @Correct, @Wrong, @Percentage, @Elapsed)";

                insertCommand.Parameters.AddWithValue("@Name", name);
                insertCommand.Parameters.AddWithValue("@Operation", operation);
                insertCommand.Parameters.AddWithValue("@Difficulty", difficulty);
                insertCommand.Parameters.AddWithValue("@Correct", correct);
                insertCommand.Parameters.AddWithValue("@Wrong", wrong);
                insertCommand.Parameters.AddWithValue("@Percentage", percentage);
                insertCommand.Parameters.AddWithValue("@Elapsed", elapsed);

                insertCommand.ExecuteNonQuery();

                return true;
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"{ex.Message}");
            return false;
        }
    }
}
