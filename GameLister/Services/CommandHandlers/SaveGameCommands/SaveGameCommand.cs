using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.SaveGameCommands;

public class SaveGameCommand : CommandHandler
{
    private readonly IProgramReader _reader;
    private readonly IGameWriteHandler _gameWriteHandler;
    protected override string BadResponse => "Invalid file.";

    public SaveGameCommand(IProgramWriter writer, IProgramReader reader,
        IGameWriteHandler gameWriteHandler) : base(writer)
    {
        _reader = reader;
        _gameWriteHandler = gameWriteHandler;
    }

    public override Command Command { get; } = new() { Name = "save game", Description = "Save new game." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter account name.");
            var accountName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            Writer.WriteLine("Enter game name.");
            var gameName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();

            _gameWriteHandler.SaveGame(accountName, gameName, out var badResponse);
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}
