using GameLister.Models.Accounts;

namespace GameLister.Services.GameListHandlers;

public interface IGameReadHandler
{
    public GamesOwner[]? GetAllGamesOwners();
    public GamesOwner[]? FindGames(string name);
}
