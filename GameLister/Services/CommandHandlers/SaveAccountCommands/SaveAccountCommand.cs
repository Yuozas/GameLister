using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.SaveAccountCommands;

public class SaveAccountCommand : CommandHandler
{
    private readonly IProgramReader _reader;
    private readonly IAccountWriteHandler _accountHandler;
    protected override string BadResponse => "Invalid file.";

    public SaveAccountCommand(IProgramWriter writer, IProgramReader reader, 
        IAccountWriteHandler accountHandler) : base(writer)
    {
        _reader = reader;
        _accountHandler = accountHandler;
    }

    public override Command Command { get; } = new() { Name = "save account", Description = "Saves account that holds games." };

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter account name.");
            var accountName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.Clear();
            _accountHandler.SaveAccount(accountName, out var badResponse);
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}