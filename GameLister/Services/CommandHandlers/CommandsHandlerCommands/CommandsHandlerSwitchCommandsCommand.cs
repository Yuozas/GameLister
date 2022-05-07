using GameLister.Models.Commands;
using GameLister.Models.GameListHolders;
using GameLister.Models.GameListHolders.Templates;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandHandlers.CommandsHandlerCommands;

public class CommandsHandlerSwitchCommandsCommand : CommandHandler
{
    private const string DEFAULT_COMMAND_NAME = "switch lister";
    private readonly IProgramReader _reader;
    private readonly GameListerHolder[] _gameListerHolders;
    private readonly GameListHolderHolder _gameListHolderHolder;
    public override Command Command { get; }

    public CommandsHandlerSwitchCommandsCommand(IProgramWriter writer, IProgramReader reader,
        GameListerHolder[] gameListerHolders, GameListHolderHolder gameListHolderHolder, string commandName = DEFAULT_COMMAND_NAME) 
        : base(writer)
    {
        _reader = reader;
        _gameListerHolders = gameListerHolders;
        _gameListHolderHolder = gameListHolderHolder;
        Command = new()
        {
            Name = commandName,
            Description = "Switch game lister."
        };
    }

    public override void Run()
    {
        try
        {
            Writer.WriteLine("Pick one of the listers:");
            foreach(var gameListerHolder in _gameListerHolders)
                Writer.WriteLine($"· {gameListerHolder.Name}");
            var gameListerHolderName = 
                    _reader.ReadLineDeclineIncorrect(Writer, _gameListerHolders.Select(holder => holder.Name).ToArray());
            _gameListHolderHolder.GameListerHolder = 
                _gameListerHolders.FirstOrDefault(gameListerHolder => gameListerHolder.Name == gameListerHolderName);
            Writer.Clear();
        }
        catch
        {
            RespondBad();
        }
    }
}
