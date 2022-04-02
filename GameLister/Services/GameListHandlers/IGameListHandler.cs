using GameLister.Models.Accounts;

namespace GameLister.Services.GameListHandlers;

public interface IGameListHandler
{
    void CreateFileIfDoesntExist();
    public GamesOwner[]? GetAllGamesOwners();
    public GamesOwner[]? FindGames(string name);
    public void SaveGame(string accountName, string gameName, out string badResponse);
    public void DeleteGame(string accountName, string gameName, out string badResponse);
    public void SaveAccount(string accountName, out string badResponse);
    public void DeleteAccount(string accountName, out string badResponse);
}
