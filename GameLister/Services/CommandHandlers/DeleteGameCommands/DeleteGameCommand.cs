using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.DeleteGameCommands;

public class DeleteGameCommand : CommandHandler
{
    protected override string BadResponse => "Invalid file.";
    private readonly IGameWriteHandler _gameWriteHandler;
    private readonly IProgramReader _reader;

    public DeleteGameCommand(IProgramWriter writer, IProgramReader reader, 
        IGameWriteHandler gameWriteHandler) : base(writer)
    {
        _gameWriteHandler = gameWriteHandler;
        _reader = reader;
    }

    public override Command Command { get; } = new() { Name = "delete game", Description = "Delete owned game." };

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

            _gameWriteHandler.DeleteGame(accountName, gameName, out string badResponse);
            if(badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}
