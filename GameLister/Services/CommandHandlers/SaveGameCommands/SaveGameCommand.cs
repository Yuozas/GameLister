using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers.SaveGameCommands.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.SaveGameCommands;

internal class SaveGameCommand : CommandHandler, ISaveGameCommand
{
    private readonly IProgramReader _reader;
    private readonly IGameListHandler _gameListHandler;
    protected override string BadResponse => "Invalid file.";

    public SaveGameCommand(LifeHandler lifeHandler, IProgramWriter writer, IProgramReader reader,
        IGameListHandler gameListHandler) : base(lifeHandler, writer)
    {
        _reader = reader;
        _gameListHandler = gameListHandler;
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

            _gameListHandler.SaveGame(accountName, gameName, out var badResponse);
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}
