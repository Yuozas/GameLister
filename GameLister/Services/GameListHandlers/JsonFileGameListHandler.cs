using GameLister.Data;
using GameLister.Extensions;
using GameLister.Models.Accounts;
using GameLister.Models.Games;
using Newtonsoft.Json;

namespace GameLister.Services.GameListHandlers;

public class JsonFileGameListHandler : IGameListHandler
{
    protected virtual string FileLocation { get; } = GameListData.FILE_LOCATION;

    public void CreateFileIfDoesntExist()
    {
        if (File.Exists(FileLocation))
            return;
        SaveFile(Array.Empty<GamesOwner>());
    }

    public GamesOwner[]? GetAllGamesOwners()
    {
        using StreamReader r = new(FileLocation);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<GamesOwner[]>(json);
    }

    public GamesOwner[]? FindGames(string name)
    {
        var gamesOwners = GetAllGamesOwners();
        if (gamesOwners.IsNullOrEmpty())
            return Array.Empty<GamesOwner>();

        List<GamesOwner> foundGamesOwners = new();
        foreach (var gamesOwner in gamesOwners)
        {
            var games = gamesOwner.Games.Where(game => game.Name.AllWordsExist(name)).ToArray() ?? Array.Empty<Game>();
            if(!games.IsNullOrEmpty())
                foundGamesOwners.Add(new() { Games = games, Account = gamesOwner.Account });
        }
        return foundGamesOwners.ToArray();
    }

    public SimilarGamesOwner[] FindSimilarGames(string name)
    {
        var gamesOwners = GetAllGamesOwners();
        if (gamesOwners.IsNullOrEmpty())
            return Array.Empty<SimilarGamesOwner>();

        List<SimilarGamesOwner> foundGamesOwners = new();
        foreach (var gamesOwner in gamesOwners)
        {
            var similarGames = gamesOwner.Games.Select(game=>new SimilarGame
            {
                Game = game,
                Similarity = game.Name.CalculateSimilarity(name)
            }).OrderBy(similarGame => similarGame.Similarity).ToArray() ?? Array.Empty<SimilarGame>();
            if (!similarGames.IsNullOrEmpty())
                foundGamesOwners.Add(new() { Games = similarGames, Account = gamesOwner.Account });
        }
        return foundGamesOwners.ToArray();
    }

    public void SaveGame(string accountName, string gameName, out string badResponse, bool errorSameGame = true)
    {
        badResponse = string.Empty;
        var allGamesOwners = GetAllGamesOwners() ?? Array.Empty<GamesOwner>();
        if (accountName.IsNullOrEmpty())
        {
            badResponse = "Invalid file.";
            return;
        }

        if (!AccountExists(allGamesOwners, accountName))
        {
            badResponse = "No such account.";
            return;
        }

        if (!allGamesOwners.IsNullOrEmpty() && GameExists(allGamesOwners, accountName, gameName))
        {
            if(errorSameGame)
                badResponse = "Game already exists.";
            return;
        }

        List<GamesOwner> newGameOwners = new();
        foreach (var gamesOwner in allGamesOwners)
        {
            if (gamesOwner.Account.Name == accountName)
            {
                var games = gamesOwner.Games.ToList();
                games.Add((Game)gameName);

                newGameOwners.Add(new()
                {
                    Account = gamesOwner.Account,
                    Games = games.ToArray()
                });
            }
            else
            {
                newGameOwners.Add(gamesOwner);
            }
                
        }

        SaveFile(newGameOwners);
    }

    public void DeleteGame(string accountName, string gameName, out string badResponse)
    {
        badResponse = string.Empty;
        var allGamesOwners = GetAllGamesOwners();
        if (accountName.IsNullOrEmpty())
        {
            badResponse = "Invalid file.";
            return;
        }

        if (!AccountExists(allGamesOwners, accountName))
        {
            badResponse = "No such account.";
            return;
        }

        if (!GameExists(allGamesOwners, accountName, gameName))
        {
            badResponse = "No such game.";
            return;
        }

        List<GamesOwner> newGameOwners = new();
        foreach (var gamesOwner in allGamesOwners)
        {
            if (gamesOwner.Account.Name == accountName)
                newGameOwners.Add(new()
                {
                    Account = gamesOwner.Account,
                    Games = gamesOwner.Games.Where(game => game.Name != gameName).ToArrayOrEmpty()
                });
            else
                newGameOwners.Add(gamesOwner);
        }

        SaveFile(newGameOwners);
    }

    public void SaveAccount(string accountName, out string badResponse, string? accountId = null)
    {
        badResponse = string.Empty;
        var allGamesOwners = GetAllGamesOwners();
        if (accountName.IsNullOrEmpty())
        {
            badResponse = "Invalid file.";
            return;
        }

        if (AccountExists(allGamesOwners, accountName))
        {
            badResponse = "Account already exists.";
            return;
        }

        List<GamesOwner> newGameOwners = allGamesOwners.ToList();
        newGameOwners.Add(new()
        {
            Account = new() { Id = accountId, Name = accountName },
            Games = Array.Empty<Game>()
        });

        SaveFile(newGameOwners);
    }

    public void DeleteAccount(string accountName, out string badResponse)
    {
        badResponse = string.Empty;
        var allGamesOwners = GetAllGamesOwners();
        if (accountName.IsNullOrEmpty())
        {
            badResponse = "Invalid file.";
            return;
        }

        if (!AccountExists(allGamesOwners, accountName))
        {
            badResponse = "No such account.";
            return;
        }

        List<GamesOwner> newGameOwners = new();
        foreach (var gamesOwner in allGamesOwners)
        {
            if (gamesOwner.Account.Name == accountName)
                continue;
            else
                newGameOwners.Add(gamesOwner);
        }

        SaveFile(newGameOwners);
    }

    private static bool GameExists(GamesOwner[] gamesOwners, string accountName, string gameName)
    {
        var gameOwner = gamesOwners.FirstOrDefault(owner=>owner.Account.Name == accountName);
        return gameOwner.Games.Any(game => game.Name == gameName);
    }

    private static bool AccountExists(GamesOwner[]? gamesOwners, string accountName)
    {
        if (gamesOwners.IsNullOrEmpty())
            return false;

        return gamesOwners.Any(gameOwner => gameOwner.Account.Name == accountName);
    }

    private void SaveFile(ICollection<GamesOwner> gameOwners)
    {
        using FileStream createStream = File.Create(FileLocation);
        using StreamWriter jsonStream = new(createStream);
        JsonSerializer jsonSerializer = new();
        jsonSerializer.Serialize(jsonStream, gameOwners);
    }
}
