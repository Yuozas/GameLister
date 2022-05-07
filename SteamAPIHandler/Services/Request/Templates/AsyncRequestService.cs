namespace SteamAPIClient.Services.Request.Templates;

public abstract class AsyncRequestService<T> : IAsyncRequestService<T>
{
    private readonly HttpClient _client = new();

    protected abstract Func<Stream, ValueTask<T>> StreamReadFunc { get; }

    public async Task<T> Get(string requestUri)
    {
        var streamTask = await _client.GetStreamAsync(requestUri);
        var repositories = await StreamReadFunc(streamTask);
        return repositories;
    }
}
