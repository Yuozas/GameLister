using GameLister.Data;

namespace GameLister.Services.GameListHandlers;

public class SteamFileGameListHandler : JsonFileGameListHandler
{
    protected override string FileLocation => GameListData.STEAM_FILE_LOCATION;
}
