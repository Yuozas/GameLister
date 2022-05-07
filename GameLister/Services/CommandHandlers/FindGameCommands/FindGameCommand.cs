using GameLister.Extensions;
using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandHandlers.FindGameCommands;

public class FindGameCommand : CommandHandler
{
    private readonly IProgramReader _reader;
    private readonly IGameReadHandler _gameReadHandler;
    protected override string BadResponse => "Invalid file.";
    public FindGameCommand(IProgramWriter writer, IProgramReader reader, 
        IGameReadHandler gameReadHandler) : base(writer)
    {
        _reader = reader;
        _gameReadHandler = gameReadHandler;
    }

    public override Command Command { get; } = new() { Name = "find games", Description = "Find all games that has same words in name." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter game name.");
            var gameName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            var gamesOwners = _gameReadHandler.FindGames(gameName);
            if (gamesOwners.IsNullOrEmpty())
            {
                Writer.WriteLine("No similar games found.");
                return;
            }
            foreach(var gamesOwner in gamesOwners)
            {
                Writer.WriteLine($"Found account {gamesOwner.Account.Name}, that owns {gamesOwner.Games.Length} similar games:");
                foreach(var game in gamesOwner.Games)
                    Writer.WriteLine($"-{game.Name}");
            }
        }
        catch
        {
            RespondBad();
        }
    }
}
