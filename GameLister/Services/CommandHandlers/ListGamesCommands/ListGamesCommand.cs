using GameLister.Data;
using GameLister.Models.Accounts;
using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers.ListGamesCommands.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;
using Newtonsoft.Json;

namespace GameLister.Services.CommandsHandlers.ListGamesCommands;

public class ListGamesCommand : CommandHandler, IListGamesCommand
{
    protected override string BadResponse { get; } = "Invalid file.";
    private readonly IGameListHandler _gameListHandler;
    public ListGamesCommand(LifeHandler lifeHandler, IProgramWriter writer, IGameListHandler gameListHandler) 
        : base(lifeHandler, writer)
    {
        _gameListHandler = gameListHandler;
    }

    public override Command Command { get; } = new() { Name = "list", Description = "List all owned games." };

    public override void Run()
    {
        try
        {
            var owners = _gameListHandler.GetAllGamesOwners();
            if (owners is null)
                RespondBad("No games owned.");
            else
                ListOwners(owners);
        }
        catch
        {
            RespondBad();
        }
    }

    private void ListOwners(GamesOwner[] owners)
    {
        foreach (var owner in owners)
        {
            Writer.WriteLine($"Account| {owner.Account.Name}");
            foreach (var game in owner.Games)
                Writer.WriteLine($"- {game.Name}");
        }
    }
}
