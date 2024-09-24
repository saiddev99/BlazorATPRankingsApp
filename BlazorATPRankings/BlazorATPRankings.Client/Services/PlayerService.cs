using BlazorATPRankings.Client.ViewModels;
using BlazorATPRankingsAPI.Models;
using System.Net.Http;
using System.Text.Json;

namespace BlazorATPRankings.Client.Services;

public class PlayerService
{
    private readonly HttpClient httpClient;

    public PlayerService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task <List<Player>> GetPlayers()
    {
        var players = await httpClient.GetStringAsync("");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<List<Player>>(players, options);
    }

    public async Task<Player> GetPlayer(int id)
    {
        var player = await httpClient.GetStringAsync($"{id}");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<Player>(player, options);
    }


    public async Task DeletePlayer(int id)
    {
        await httpClient.DeleteAsync($"/delete/{id}");
    }

    public async Task<PlayerViewModel> AddPlayer(PlayerViewModel player)
    {
        HttpResponseMessage message = await httpClient.PostAsJsonAsync<PlayerViewModel>(
        $"add", player);
        if (message.IsSuccessStatusCode)
        {
            return player;
        }
        else
        {
            return null;
        }    
    }
}
