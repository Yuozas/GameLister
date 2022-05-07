using GameLister.Extensions;
using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandHandlers.FindGameCommands;

public class FindGameCommand : CommandHandler
{
    protected readonly IProgramReader Reader;
    protected readonly IGameReadHandler GameReadHandler;
    protected override string BadResponse => "Invalid file.";
    public FindGameCommand(IProgramWriter writer, IProgramReader reader, 
        IGameReadHandler gameReadHandler) : base(writer)
    {
        Reader = reader;
        GameReadHandler = gameReadHandler;
    }

    public override Command Command { get; } = new() { Name = "find games", Description = "Find all games that has same words in name." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter game name.");
            var gameName = Reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            var gamesOwners = GameReadHandler.FindGames(gameName);
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
