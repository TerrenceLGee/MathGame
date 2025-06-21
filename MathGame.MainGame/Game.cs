using MathGame.MainGame.Options;
using MathLibrary;

namespace MathGame.MainGame;
public class Game
{
    private Random _rand = new Random();

    public Game()
    {
    }
    public (int x, int y) GetValidOperands(GameOption option, DifficultyLevel level)
    {
        (int startRange, int endRange) = GetProperRange(level);
        int x = _rand.Next(startRange, endRange);
        int y = _rand.Next(startRange, endRange);

        while (!IsValidForDivisionOrModulus(x, y))
        {
            x = _rand.Next(startRange, endRange);
            y = _rand.Next(startRange, endRange);
        }

        return (x, y);
    }

    private (int start, int end) GetProperRange(DifficultyLevel level)
    {
        int start = 1;

        int end = level switch
        {
            DifficultyLevel.Medium => 100,
            DifficultyLevel.Hard => 1000,
            DifficultyLevel.Legend => 10_000,
            _ => 10,
        };

        return (start, end);
    }

    private bool IsValidForDivisionOrModulus(int x, int y)
    {
        return x > y;
    }

    private GameOption HandleRandomChoice()
    {
        GameOption[] options = new GameOption[]
        {
            GameOption.Addition,
            GameOption.Subtraction,
            GameOption.Multiplication,
            GameOption.Division,
            GameOption.Mod,
        };

        return options[_rand.Next(0, options.Length)];
    }

    public int GetCorrectAnswer(GameOption option, int x, int y)
    {
        List<MathProblem> mathMethods = new List<MathProblem>();
        mathMethods.Add(MathOperations.Add);
        mathMethods.Add(MathOperations.Subtract);
        mathMethods.Add(MathOperations.Multiply);
        mathMethods.Add(MathOperations.Divide);
        mathMethods.Add(MathOperations.Mod);

        return option switch
        {
            GameOption.Addition => mathMethods[0](x, y),
            GameOption.Subtraction => mathMethods[1](x, y),
            GameOption.Multiplication => mathMethods[2](x, y),
            GameOption.Division => mathMethods[3](x, y),
            GameOption.Mod => mathMethods[4](x, y),
        };
    }

    public string GetQuestionForDisplay(GameOption option, int operandA, int operandB)
    {
        char sign = option switch
        {
            GameOption.Addition => '+',
            GameOption.Subtraction => '-',
            GameOption.Multiplication => '*',
            GameOption.Division => '/',
            GameOption.Mod => '%',
        };

        char equals = '=';

        return $"{operandA} {sign} {operandB} {equals} ";
    }

    public (int answer, string question, GameOption option) InCaseOfRandomGame(int x, int y)
    {
        GameOption option = HandleRandomChoice();
        int answer = GetCorrectAnswer(option, x, y);
        string question = GetQuestionForDisplay(option, x, y);
        return (answer, question, option);
    }

    public bool IsAnswerCorrect(int userAnswer, int actualAnswer)
    {
        return userAnswer == actualAnswer;
    }

    private delegate int MathProblem(int x, int y);
}
