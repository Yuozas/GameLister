using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.DeleteAccountCommands;

public class DeleteAccountCommand : CommandHandler
{
    private readonly IProgramReader _reader;
    private readonly IAccountWriteHandler _accountHandler;

    public DeleteAccountCommand(IProgramWriter writer, IProgramReader reader, 
        IAccountWriteHandler accountHandler) : base(writer)
    {
        _reader = reader;
        _accountHandler = accountHandler;
    }

    public override Command Command { get; } = new() { Name = "delete account", Description = "Deletes account that owns games." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter account name.");
            var accountName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            _accountHandler.DeleteAccount(accountName, out var badResponse);
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad("Failed to delete account, unhandled exception...");
        }
    }
}
