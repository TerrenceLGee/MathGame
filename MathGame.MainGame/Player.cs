using MathGame.MainGame.Options;

namespace MathGame.MainGame;
public class Player
{
    private static int _id = 0;
    public int ID { get; set; }
    public string? Name { get; set; }
    public GameOption GameOperation { get; set; }
    public DifficultyLevel Level { get; set; }
    public int NumberCorrect { get; set; }
    public int NumberWrong { get; set; }
    public double PercentageCorrect { get; set; }

    public Player(string? name, GameOption operation, DifficultyLevel level, int numberCorrect, int numberWrong)
    {
        ID = _id++;
        Name = name;
        GameOperation = operation;
        Level = level;
        NumberCorrect = numberCorrect;
        NumberWrong = numberWrong;
    }
}
