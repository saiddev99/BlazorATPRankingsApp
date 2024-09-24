using System.Text.Json.Serialization;

namespace BlazorATPRankingsAPI.DTO;

public class PlayerDTO
{
    internal int Id { get; set; }
    internal int Rank { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public int Points { get; set; }
}
