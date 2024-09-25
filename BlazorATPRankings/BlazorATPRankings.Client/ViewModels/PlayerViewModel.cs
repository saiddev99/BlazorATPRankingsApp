using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorATPRankings.Client.ViewModels;

public class PlayerViewModel
{
    [JsonPropertyName("player")]
    [Required(ErrorMessage = "{0} is een verplicht veld")] 
    public string Name { get; set; }

    [JsonPropertyName("country")]
    [Required(ErrorMessage = "{0} is een verplicht veld")]
    public string Country { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }
}
