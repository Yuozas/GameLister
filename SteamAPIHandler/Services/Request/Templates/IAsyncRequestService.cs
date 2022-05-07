namespace SteamAPIClient.Services.Request.Templates;

public interface IAsyncRequestService<T>
{
    public Task<T> Get(string requestUri);
}
