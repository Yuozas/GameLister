namespace GameLister.Services.GameListHandlers;

public interface IGameWriteHandler
{
    public void SaveGame(string accountName, string gameName, out string badResponse, bool errorSameGame = true);
    public void DeleteGame(string accountName, string gameName, out string badResponse);
}
