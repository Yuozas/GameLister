using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.SaveAccountCommands;

public class SaveSteamAccountCommand : SaveAccountCommand
{
    private readonly IProgramReader _reader;
    private readonly IAccountWriteHandler _accountHandler;

    public SaveSteamAccountCommand(IProgramWriter writer, IProgramReader reader, 
        IAccountWriteHandler accountHandler) : base(writer, reader, accountHandler)
    {
        _reader = reader;
        _accountHandler = accountHandler;
    }

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Enter steam username.");
            var accountName = _reader.ReadLineDeclineEmpty(Writer);
            Writer.WriteLine("Enter steam id.");
            var steamId = _reader.ReadNumberLineDeclineEmpty(Writer);
            Writer.Clear();
            _accountHandler.SaveAccount(accountName, out var badResponse, steamId.ToString());
            if (badResponse is not null)
                RespondBad(badResponse);
        }
        catch
        {
            RespondBad();
        }
    }
}