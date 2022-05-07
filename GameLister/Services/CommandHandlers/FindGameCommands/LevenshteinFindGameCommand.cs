using GameLister.Extensions;
using GameLister.Models.Commands;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandHandlers.FindGameCommands;

public class LevenshteinFindGameCommand : FindGameCommand
{
    public LevenshteinFindGameCommand(IProgramWriter writer, IProgramReader reader, IGameReadHandler gameReadHandler) : base(writer, reader, gameReadHandler)
    {
    }

    public override Command Command { get; } = new() { Name = "find similar games", Description = "Find similar named games." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter game name.");
            var gameName = Reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            var gamesOwners = GameReadHandler.FindSimilarGames(gameName);
            if (gamesOwners.IsNullOrEmpty())
            {
                Writer.WriteLine("No similar games found.");
                return;
            }
            var sortedInfo = gamesOwners.GetSortedInfo().ToArray();
            foreach (var info in sortedInfo)
                Writer.WriteLine(info);
        }
        catch
        {
            RespondBad();
        }
    }
}
