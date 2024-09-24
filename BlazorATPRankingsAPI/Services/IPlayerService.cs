using BlazorATPRankingsAPI.DTO;
using BlazorATPRankingsAPI.Models;

namespace BlazorATPRankingsAPI.Services;

public interface IPlayerService
{
    List<Player> GetPlayers();
    List<Player> SearchPlayers(string name);
    Player GetPlayer(int id);
    Player AddPlayer(PlayerDTO player);
    Player UpdatePlayer(int Id, PlayerDTO player);
    Player DeletePlayer(int id);
}
