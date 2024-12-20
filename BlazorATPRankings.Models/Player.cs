﻿using System.Text.Json.Serialization;

namespace BlazorATPRankingsAPI.Models;

public class Player
{
    public int Id { get; set; }

    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("player")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }
}
