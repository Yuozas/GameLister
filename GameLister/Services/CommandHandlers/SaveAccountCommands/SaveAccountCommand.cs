using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers.SaveAccountCommands.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.SaveAccountCommands;

internal class SaveAccountCommand : CommandHandler, ISaveAccountCommand
{
    private readonly IProgramReader _reader;
    private readonly IGameListHandler _gameListHandler;
    protected override string BadResponse => "Invalid file.";

    public SaveAccountCommand(LifeHandler lifeHandler, IProgramWriter writer, IProgramReader reader
        , IGameListHandler gameListHandler) : base(lifeHandler, writer)
    {
        _reader = reader;
        _gameListHandler = gameListHandler;
    }

    public override Command Command { get; } = new() { Name = "save account", Description = "Saves account that holds games." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter account name.");
            var accountName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            _gameListHandler.SaveAccount(accountName, out var badResponse);
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}
