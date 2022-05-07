using SteamAPIClient.Extensions;
using SteamAPIClient.Helpers.Api.Templates;
using SteamAPIClient.Models.OwnedGames;
using SteamAPIClient.Services.Request;
using SteamAPIClient.Services.Request.Templates;
using SteamAPIClient.Services.Steam.Templates;

namespace SteamAPIClient.Services.Steam;

public class PlayerService : IPlayerService
{
    private readonly ISteamUrlBuilder _urlBuilder;

    public PlayerService(ISteamUrlBuilder urlBuilder)
    {
        _urlBuilder = urlBuilder;
    }

    public async Task<Root> GetOwnedGames(long steamId, bool skipUnvettedApps = false, bool includeAppInfo = true, 
        bool includePlayedFreeGames = true, bool includeFreeSub = false)
    {
        var @params = new Dictionary<string, string>()
        {
            ["skip_unvetted_apps"] = skipUnvettedApps.ToIntString(),
            ["include_appinfo"] = includeAppInfo.ToIntString(),
            ["include_played_free_games"] = includePlayedFreeGames.ToIntString(),
            ["include_free_sub"] = includeFreeSub.ToIntString()
        };

        var url = _urlBuilder.GetApiUrl(nameof(IPlayerService), nameof(GetOwnedGames), steamId, @params);
        IAsyncRequestService<Root> requestService = new JsonAsyncRequestService<Root>();

        var result = await requestService.Get(url);
        return result;
    }
}
