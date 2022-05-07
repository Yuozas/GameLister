using System.Text.Json.Serialization;

namespace SteamAPIClient.Models.OwnedGames;

public class Game
{
    [JsonPropertyName("appid")]
    public int AppId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("playtime_forever")]
    public int PlaytimeForever { get; init; }

    [JsonPropertyName("img_icon_url")]
    public string ImgIconUrl { get; init; } = string.Empty;

    [JsonPropertyName("playtime_windows_forever")]
    public int PlaytimeWindowsForever { get; init; }

    [JsonPropertyName("playtime_mac_forever")]
    public int PlaytimeMacForever { get; init; }

    [JsonPropertyName("playtime_linux_forever")]
    public int PlaytimeLinuxForever { get; init; }

    [JsonPropertyName("has_community_visible_stats")]
    public bool? HasCommunityVisibleStats { get; init; }
}
