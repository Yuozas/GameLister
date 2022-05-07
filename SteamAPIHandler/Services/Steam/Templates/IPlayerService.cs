using SteamAPIClient.Models.OwnedGames;

namespace SteamAPIClient.Services.Steam.Templates;

public interface IPlayerService
{
    public Task<Root> GetOwnedGames(long steamId, bool skipUnvettedApps = false, bool includeAppInfo = true,
        bool includePlayedFreeGames = true, bool includeFreeSub = false);
}
