using MathGame.MainGame.Options;

namespace MathGame.MainGame;
public class Player
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public GameOption GameOperation { get; set; }
    public DifficultyLevel Level { get; set; }
    public int NumberCorrect { get; set; }
    public int NumberWrong { get; set; }
    public double PercentageCorrect { get; set; }

    public Player(string? name, GameOption operation, DifficultyLevel level, int numberCorrect, int numberWrong, double percentageCorrect)
    {
        Name = name;
        GameOperation = operation;
        Level = level;
        NumberCorrect = numberCorrect;
        NumberWrong = numberWrong;
        PercentageCorrect = percentageCorrect;
    }
}
