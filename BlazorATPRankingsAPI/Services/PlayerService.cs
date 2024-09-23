using BlazorATPRankingsAPI.Models;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace BlazorATPRankingsAPI.Services;

public class PlayerService: IPlayerService
{
    private string playerJson = new StreamReader("./Assets/atp.json").ReadToEnd();
    public List<Player> playerData { get; set; }

    public PlayerService()
    {
        playerData = JsonSerializer.Deserialize<List<Player>>(playerJson)!;
        playerData.ForEach(x => x.Id = playerData.Max(x => x.Id) + 1);
        playerData.OrderByDescending(x => x.Points).ToList().ForEach(x => x.Rank = playerData.Max(x => x.Rank) + 1);
    }

    List<Player> IPlayerService.GetPlayers()
    {
        return playerData.OrderBy(x => x.Rank).ToList();
    }

    Player IPlayerService.GetPlayer(int id)
    {
        return playerData.FirstOrDefault(x => x.Id == id);
    }

    Player IPlayerService.AddPlayer(Player player)
    {
        player.Id = playerData.Max(x => x.Id) + 1;
        player.Rank = playerData.OrderByDescending(x => x.Points).FirstOrDefault(x => player.Points >= x.Points).Rank;
        playerData.OrderByDescending(x => x.Points).Where(x => (player.Points >= x.Points && player.Id != x.Id)).ToList().ForEach(x => ++x.Rank);

        playerData.Add(player);
        return player;
    }

    Player IPlayerService.UpdatePlayer(int id, Player player)
    {
        var result = playerData.FirstOrDefault(x => x.Id == id);

        if (result != null)
        {
            result.Name = player.Name;
            result.Country = player.Country;
            result.Rank = playerData.OrderByDescending(x => x.Points).FirstOrDefault(x => player.Points >= x.Points).Rank;
            playerData.OrderByDescending(x => x.Points).Where(x => (player.Points >= x.Points && id != x.Id)).ToList().ForEach(x => ++x.Rank);
            result.Points = player.Points;
        

            return result;
        }

        return null;
    }

    Player IPlayerService.DeleteContact(int id)
    {
        var result = playerData.FirstOrDefault(x => x.Id == id);

        if (result != null)
        {
            playerData.Remove(result);
            playerData.OrderByDescending(x => x.Points).Where(x => (result.Points >= x.Points && result.Id != x.Id)).ToList().ForEach(x => --x.Rank);
            return result;
        }
        return null;
    }

    public List<Player> SearchPlayers(string name)
    {
        return playerData.Where(x => x.Name.Contains(name)).ToList();
    }
}
