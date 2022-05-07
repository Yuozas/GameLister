using Newtonsoft.Json;
using SteamAPIClient.Data;
using SteamAPIClient.Models.Api;

namespace SteamAPIClient.Services.Api;

public class PropertiesReadService : IReadProperties, IWriteProperties
{
    private readonly string FileLocation = ApiData.API_KEY_FILE_LOCATION;

    public Properties GetProperties()
    {
        CreateFileIfDoesntExist();
        using StreamReader r = new(FileLocation);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<Properties>(json) ?? new();
    }

    public void SaveProperties(Properties properties)
    {
        using FileStream createStream = File.Create(FileLocation);
        using StreamWriter jsonStream = new(createStream);
        JsonSerializer jsonSerializer = new();
        jsonSerializer.Serialize(jsonStream, properties);
    }

    private void CreateFileIfDoesntExist()
    {
        if (File.Exists(FileLocation))
            return;
        SaveProperties(new());
    }
}
