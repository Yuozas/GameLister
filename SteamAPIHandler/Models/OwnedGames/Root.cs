using System.Text.Json.Serialization;

namespace SteamAPIClient.Models.OwnedGames;

public class Root
{
    [JsonPropertyName("response")]
    public Response Response { get; init; } = new();
}
