using GameLister.Services.CommandHandlers.FindGameCommands.Templates;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers.DeleteAccountCommands.Templates;
using GameLister.Services.CommandsHandlers.DeleteGameCommands.Templates;
using GameLister.Services.CommandsHandlers.ExitProgramCommands.Templates;
using GameLister.Services.CommandsHandlers.ListGamesCommands.Templates;
using GameLister.Services.CommandsHandlers.SaveAccountCommands.Templates;
using GameLister.Services.CommandsHandlers.SaveGameCommands.Templates;

namespace GameLister.Models.CommandsHolders;

public class GameListCommandsHolder : ICommandsHolder
{
    private readonly IExitProgramCommand _exitProgramCommand;
    private readonly IListGamesCommand _listGamesCommand;
    private readonly ISaveGameCommand _saveGameCommand;
    private readonly IDeleteGameCommand _deleteGameCommand;
    private readonly ISaveAccountCommand _saveAccountCommand;
    private readonly IDeleteAccountCommand _deleteAccountCommand;
    private readonly IFindGameCommand _findGameCommand;
    private readonly ICommandHandler[] _commands;
    public ICommandHandler[] Commands => _commands;

    public GameListCommandsHolder(IExitProgramCommand exitProgramCommand, IListGamesCommand listGamesCommand,
        ISaveGameCommand saveGameCommand, IDeleteGameCommand deleteGameCommand,
        ISaveAccountCommand saveAccountCommand, IDeleteAccountCommand deleteAccountCommand,
        IFindGameCommand findGameCommand)
    {
        _exitProgramCommand = exitProgramCommand;
        _listGamesCommand = listGamesCommand;
        _saveGameCommand = saveGameCommand;
        _deleteGameCommand = deleteGameCommand;
        _saveAccountCommand = saveAccountCommand;
        _deleteAccountCommand = deleteAccountCommand;
        _findGameCommand = findGameCommand;

        _commands = new ICommandHandler[]
        {
                _exitProgramCommand,
                _listGamesCommand,
                _saveGameCommand,
                _deleteGameCommand,
                _saveAccountCommand,
                _deleteAccountCommand,
                _findGameCommand,
        };
    }
}
