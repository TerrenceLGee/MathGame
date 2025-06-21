using MathGame.MainGame;
using MathGame.MainGame.DataAccess;
using MathGame.UI.Cli;
using Microsoft.Extensions.Configuration;

string jsonFile = "appsettings.json";
string connectionPhrase = "DatabaseConnection";
string? connectionString = GetConnectionString(jsonFile, connectionPhrase);

DatabaseInitializer dbInit = new DatabaseInitializer(connectionString);


Game game = new Game();
GameClub club = new GameClub();
UserInterface ui = new UserInterface(game, club);

ui.Run();


string? GetConnectionString(string jsonFile, string connectionPhrase)
{
    IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile(jsonFile)
        .Build();

    return config
        .GetConnectionString(connectionPhrase);
}