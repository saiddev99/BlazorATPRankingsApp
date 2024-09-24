using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorATPRankings.Client.ViewModels;

public class PlayerViewModel
{
    [Required(ErrorMessage = "{0} is een verplicht veld")] 
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is een verplicht veld")]
    public string Country { get; set; }

    [Required(ErrorMessage = "{0} is een verplicht veld")]
    [RegularExpression("([1-9][0-9]*)",ErrorMessage = "{0} moet een nummer zijn.")]
    public int Points { get; set; }
}
