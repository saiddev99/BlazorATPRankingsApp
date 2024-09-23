using BlazorATPRankingsAPI.Models;

namespace BlazorATPRankingsAPI.Services;

public interface IPlayerService
{
    List<Player> GetPlayers();
    List<Player> SearchPlayers(string name);
    Player GetPlayer(int id);
    Player AddPlayer(Player player);
    Player UpdatePlayer(int Id, Player player);
    Player DeleteContact(int id);
}
