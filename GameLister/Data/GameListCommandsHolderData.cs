using GameLister.Models.CommandsHolders;
using GameLister.Services.CommandHandlers.FindGameCommands;
using GameLister.Services.CommandsHandlers.DeleteAccountCommands;
using GameLister.Services.CommandsHandlers.DeleteGameCommands;
using GameLister.Services.CommandsHandlers.ExitProgramCommands;
using GameLister.Services.CommandsHandlers.ListGamesCommands;
using GameLister.Services.CommandsHandlers.SaveAccountCommands;
using GameLister.Services.CommandsHandlers.SaveGameCommands;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Data;

public class GameListCommandsHolderData : GameListCommandsHolder
{
    public GameListCommandsHolderData(LifeHandler lifeHandler, IProgramWriter programWriter, IProgramReader reader, 
        IGameListHandler gameListHandler) :
        base(
            new ExitProgramCommand(lifeHandler, programWriter),
            new ListGamesCommand(lifeHandler, programWriter, gameListHandler),
            new SaveGameCommand(lifeHandler, programWriter, reader, gameListHandler),
            new DeleteGameCommand(lifeHandler, programWriter, reader, gameListHandler),
            new SaveAccountCommand(lifeHandler, programWriter, reader, gameListHandler),
            new DeleteAccountCommand(lifeHandler, programWriter, reader, gameListHandler),
            new FindGameCommand(lifeHandler, programWriter, reader, gameListHandler)
            )
    {
    }
}
