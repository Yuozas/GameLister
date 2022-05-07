namespace GameLister.Services.GameListHandlers;

public interface IAccountWriteHandler
{
    public void SaveAccount(string accountName, out string badResponse, string? accountId = null);
    public void DeleteAccount(string accountName, out string badResponse);
}
