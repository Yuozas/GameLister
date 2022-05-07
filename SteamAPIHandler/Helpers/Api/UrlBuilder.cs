using Microsoft.AspNetCore.WebUtilities;
using SteamAPIClient.Helpers.Api.Templates;
using SteamAPIClient.Models.Api;

namespace SteamAPIClient.Helpers.Api;

public class UrlBuilder : ISteamUrlBuilder
{
    private const string API_LINK = "api.steampowered.com";
    private const string API_VERSION = "v1";

    private readonly string _apiKey;

    public UrlBuilder(Properties apiProperties)
    {
        _apiKey = apiProperties.Key;
    }

    public string GetApiUrl(string serviceName, string methodName, long steamId, Dictionary<string, string> @params)
    {
        var url = $"https://" + string.Join('/', new[] { API_LINK, serviceName, methodName, API_VERSION }) + "/";

        var allParams = GetDefaultParams();
        allParams.TryAdd("steamid", steamId.ToString());
        foreach (var (key, value) in @params)
            allParams.TryAdd(key, value);

        var newUrl = new Uri(QueryHelpers.AddQueryString(url, allParams));
        return newUrl.AbsoluteUri;
    }

    private Dictionary<string, string> GetDefaultParams()
    {
        return new Dictionary<string, string>()
        {
            ["key"] = _apiKey,
        };
    }
}
