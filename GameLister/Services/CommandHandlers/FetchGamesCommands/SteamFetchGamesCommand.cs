using GameLister.Extensions;
using GameLister.Models.Accounts;
using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;
using SteamAPIClient.Models.OwnedGames;
using SteamAPIClient.Services.Steam.Templates;
using System.Collections.Concurrent;

namespace GameLister.Services.CommandHandlers.FetchGamesCommands;

public class SteamFetchGamesCommand : CommandHandler
{
    private readonly IGameReadHandler _gameReadHandler;
    private readonly IGameWriteHandler _gameWriteHandler;
    private readonly IPlayerService _playerService;

    public SteamFetchGamesCommand(IProgramWriter writer, IGameReadHandler gameReadHandler, 
        IGameWriteHandler gameWriteHandler, IPlayerService playerService) : base(writer)
    {
        _gameReadHandler = gameReadHandler;
        _gameWriteHandler = gameWriteHandler;
        _playerService = playerService;
    }

    public override Command Command { get; } = new()
    {
        Name = "fetch games",
        Description = "Save steam games for each account."
    };

    public override async void Run()
    {
        try {
            var accounts = _gameReadHandler.GetAllGamesOwners()?.Select(gameOwner => gameOwner.Account)
                .Where(account => account.Id is not null).ToArrayOrEmpty();
            if (accounts.IsNullOrEmpty())
            {
                RespondBad("No accounts found.");
                return;
            }

            ConcurrentBag<(Root GameCollection, Account Account)> collections = new();

            await Task.WhenAll(accounts.Select(async account =>
            {
                var accountId = long.Parse(account.Id ?? "");
                var gameCollection = await _playerService.GetOwnedGames(accountId);
                collections.Add((gameCollection, account));
            }));

            foreach (var collection in collections)
            {
                foreach (var game in collection.GameCollection.Response.Games)
                {
                    _gameWriteHandler.SaveGame(collection.Account.Name, game.Name, out var badResponse, false);
                    if (badResponse is not null)
                        RespondBad(badResponse);
                }
            }
        }
        catch
        {
            RespondBad("Failed to fetch games, unhandled exception...");
        }
    }
}
