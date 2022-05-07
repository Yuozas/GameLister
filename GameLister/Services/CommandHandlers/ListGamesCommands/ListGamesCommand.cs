using GameLister.Models.Accounts;
using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.ListGamesCommands;

public class ListGamesCommand : CommandHandler
{
    protected override string BadResponse { get; } = "Invalid file.";
    private readonly IGameReadHandler _gameReadHandler;
    public ListGamesCommand(IProgramWriter writer, IGameReadHandler gameReadHandler) 
        : base(writer)
    {
        _gameReadHandler = gameReadHandler;
    }

    public override Command Command { get; } = new() { Name = "list", Description = "List all owned games." };

    public override void Run()
    {
        try
        {
            var owners = _gameReadHandler.GetAllGamesOwners();
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
