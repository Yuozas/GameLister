namespace SteamAPIClient.Helpers.Api.Templates;

public interface ISteamUrlBuilder
{
    public string GetApiUrl(string serviceName, string methodName, long steamId, Dictionary<string, string> @params);
}
