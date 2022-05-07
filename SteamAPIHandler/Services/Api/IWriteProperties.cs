using SteamAPIClient.Models.Api;

namespace SteamAPIClient.Services.Api;

public interface IWriteProperties
{
    public void SaveProperties(Properties properties);
}
