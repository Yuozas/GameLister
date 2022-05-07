using System.Text.Json.Serialization;

namespace SteamAPIClient.Models.OwnedGames;

public class Response
{
    [JsonPropertyName("game_count")]
    public int GameCount { get; init; }

    [JsonPropertyName("games")]
    public IReadOnlyList<Game> Games { get; init; } = new List<Game>();
}
