using MathGame.MainGame;
using MathGame.MainGame.Options;
using Spectre.Console;



namespace MathGame.UI.Cli;

public class UserInterface
{
    private Game _game;
    private GameClub _club;

    public UserInterface(Game game, GameClub club)
    {
        _game = game;
        _club = club;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            int numberCorrect = 0;
            int numberIncorrect = 0;
            
            GameOption option = GetGameOption();

            if (option == GameOption.Exit) break;

            if (option == GameOption.PreviousGames)
            {
                DisplayStatistics(_club);
                ClearTheScreen();
                continue;
            }

            DifficultyLevel level = GetDifficultyLevel();
            string name = GetPlayerName();
            Console.Clear();
            int numberOfQuestions = GetNumberOfQuestions();
            Console.Clear();
            string question = string.Empty;
            int correctAnswer, userAnswer;

            for (int i = 0; i < numberOfQuestions; i++)
            {
                (int x, int y) = _game.GetValidOperands(option, level);
                if (option == GameOption.Random)
                {
                    (correctAnswer, question, GameOption randomOption) = _game.InCaseOfRandomGame(x, y);
                }
                else
                {
                    question = _game.GetQuestionForDisplay(option, x, y);
                    correctAnswer = _game.GetCorrectAnswer(option, x, y);
                }
                userAnswer = GetUserAnswer(question);
                _ = _game.IsAnswerCorrect(userAnswer, correctAnswer)
                    ? numberCorrect++
                    : numberIncorrect++;
                Console.Clear();
            }

            Player player = new Player(name, option, level, numberCorrect, numberIncorrect);
            player.PercentageCorrect = (double)(numberCorrect / numberOfQuestions) * 100.0;

            _club.GamePlayers.Add(player);
        }

        ClearTheScreen();
    }

    private string GetPlayerName()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("May I have your name: "));
    }

    private int GetNumberOfQuestions()
    {
        return AnsiConsole.Prompt(new TextPrompt<int>("How many math problems would you like to solve? "));
    }

    private int GetUserAnswer(string question)
    {
        return AnsiConsole.Prompt(new TextPrompt<int>(question));
    }
    private GameOption GetGameOption()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<GameOption>()
            .Title("Please select an operation to perform or exit to quit.")
            .AddChoices(
                GameOption.Addition,
                GameOption.Subtraction,
                GameOption.Multiplication,
                GameOption.Division, 
                GameOption.Mod, 
                GameOption.Random, 
                GameOption.PreviousGames,
                GameOption.Exit));
    }

    private static DifficultyLevel GetDifficultyLevel()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<DifficultyLevel>()
            .Title("Please choose the level of difficulty")
            .AddChoices(
                DifficultyLevel.Easy,
                DifficultyLevel.Medium,
                DifficultyLevel.Hard,
                DifficultyLevel.Legend));
    }

    private void ClearTheScreen()
    {
        AnsiConsole.MarkupLine("[cyan2]Press any key to continue[/]");
        Console.ReadKey();
        Console.Clear();
    }

    public void DisplayStatistics(GameClub club)
    {
        if (!club.GamePlayers.Any())
        {
            AnsiConsole.MarkupLine("[darkred]There are currently no games on record to display[/]");
            //ClearTheScreen();
            return;
        }

        Console.Clear();
        Table table = new Table();
        table.Border(TableBorder.Heavy);
        table.AddColumn("[darkgoldenrod]Name[/]");
        table.AddColumn("[darkgoldenrod]Math Operation[/]");
        table.AddColumn("[darkgoldenrod]Difficulty Level[/]");
        table.AddColumn("[darkgoldenrod]Number Correct[/]");
        table.AddColumn("[darkgoldenrod]Number Wrong[/]");
        table.AddColumn("[darkgoldenrod]Percentage Correct[/]");

        foreach (Player player in club.GamePlayers)
        {
            table.AddRow(
                $"{player.Name}",
                $"{player.GameOperation}",
                $"{player.Level}",
                $"{player.NumberCorrect}",
                $"{player.NumberWrong}",
                $"{player.PercentageCorrect/100:P}");
        }

        AnsiConsole.Write(table);
    }

}
