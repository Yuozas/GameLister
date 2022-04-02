using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers.DeleteAccountCommands.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.DeleteAccountCommands;

public class DeleteAccountCommand : CommandHandler, IDeleteAccountCommand
{
    private readonly IProgramReader _reader;
    private readonly IGameListHandler _gameListHandler;

    public DeleteAccountCommand(LifeHandler lifeHandler, IProgramWriter writer, 
        IProgramReader reader, IGameListHandler gameListHandler) : base(lifeHandler, writer)
    {
        _reader = reader;
        _gameListHandler = gameListHandler;
    }

    public override Command Command { get; } = new() { Name = "delete account", Description = "Deletes account that owns games." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter account name.");
            var accountName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            _gameListHandler.DeleteAccount(accountName, out var badResponse);
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}
