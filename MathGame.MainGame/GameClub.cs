using MathGame.MainGame.DataAccess;

namespace MathGame.MainGame;
public class GameClub
{
    public List<Player> GamePlayers { get; set; }

    public GameClub()
    {
        GamePlayers = DataRetrieval.RetrievePlayers();
    }


    public bool SavePlayer(Player player)
    {
        return DataInsertion.InsertPlayer(player);
    }

}
