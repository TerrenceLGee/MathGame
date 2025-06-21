using MathGame.MainGame;
using MathGame.UI.Cli;


Game game = new Game();
GameClub club = new GameClub();
UserInterface ui = new UserInterface(game, club);

ui.Run();
