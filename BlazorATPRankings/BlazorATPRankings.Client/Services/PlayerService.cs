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

    public async Task DeleteContact(int id)
    {
        await httpClient.DeleteAsync($"/delete/{id}");
    }
}
