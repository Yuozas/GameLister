using SteamAPIClient.Services.Request.Templates;
using System.Text.Json;

namespace SteamAPIClient.Services.Request;

public class JsonAsyncRequestService<T> : AsyncRequestService<T>
{
    protected override Func<Stream, ValueTask<T>> StreamReadFunc
    {
        get
        {
            return async (stream) => await JsonSerializer.DeserializeAsync<T>(stream) ?? throw new Exception("Stream result cannot be null.");
        }
    }
}
