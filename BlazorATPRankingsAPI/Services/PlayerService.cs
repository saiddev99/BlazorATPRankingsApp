﻿using BlazorATPRankingsAPI.DTO;
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

    Player IPlayerService.AddPlayer(PlayerDTO player)
    {
        int rank = 0;

        if (playerData.OrderBy(x => x.Rank).FirstOrDefault(x => player.Points >= x.Points) == null)
        {
            rank = playerData.OrderBy(x => x.Rank).Last().Rank + 1;
        }
        else
        {
            rank = playerData.OrderBy(x => x.Rank).FirstOrDefault(x => player.Points >= x.Points).Rank;
        }


        Player playertoAdd = new()
        {
            Id = playerData.Max(x => x.Id) + 1,
            Rank = rank,
            Name = player.Name,
            Country = player.Country,
            Points = player.Points
        };
        playerData.OrderBy(x => x.Rank).Where(x => (player.Points >= x.Points)).ToList().ForEach(x => ++x.Rank);
        playerData.Add(playertoAdd);
        return playertoAdd;
    }

    Player IPlayerService.UpdatePlayer(int id, PlayerDTO player)
    {
        var result = playerData.FirstOrDefault(x => x.Id == id);

        int rank = 0;

        if (playerData.OrderBy(x => x.Rank).FirstOrDefault(x => player.Points >= x.Points) == null)
        {
            rank = playerData.OrderBy(x => x.Rank).Last().Rank;
            playerData.OrderBy(x => x.Rank).Where(x => result.Points > x.Points).ToList().ForEach(x => --x.Rank);
        }
        else
        {
            rank = playerData.OrderBy(x => x.Rank).FirstOrDefault(x => player.Points >= x.Points).Rank;
        }

        if (result != null)
        {
            result.Name = player.Name;
            result.Country = player.Country;
            result.Rank = rank;
            result.Points = player.Points;

            playerData.OrderBy(x => x.Rank).Where(x => (result.Points >= x.Points && result.Id != x.Id)).ToList().ForEach(x => ++x.Rank);
            return result;
        }

        return null;
    }

    Player IPlayerService.DeletePlayer(int id)
    {
        var result = playerData.FirstOrDefault(x => x.Id == id);

        if (result != null)
        {
            playerData.Remove(result);
            playerData.OrderBy(x => x.Rank).Where(x => (result.Rank <= x.Rank)).ToList().ForEach(x => --x.Rank);
            return result;
        }
        return null;
    }

    public List<Player> SearchPlayers(string name)
    {
        return playerData.Where(x => x.Name.ToLower().Contains(name)).ToList();
    }
}
