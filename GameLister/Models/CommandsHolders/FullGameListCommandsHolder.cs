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

public class FullGameListCommandsHolder : CommandsHolder
{
    public FullGameListCommandsHolder(LifeHandler lifeHandler, IProgramWriter programWriter, IProgramReader reader,
        IAccountWriteHandler accountHandler, IGameReadHandler gameReadHandler, IGameWriteHandler gameWriteHandler) :
        base(
            new ExitProgramCommand(lifeHandler, programWriter),
            new ListGamesCommand(programWriter, gameReadHandler),
            new SaveGameCommand(programWriter, reader, gameWriteHandler),
            new DeleteGameCommand(programWriter, reader, gameWriteHandler),
            new SaveAccountCommand(programWriter, reader, accountHandler),
            new DeleteAccountCommand(programWriter, reader, accountHandler),
            new FindGameCommand(programWriter, reader, gameReadHandler),
            new LevenshteinFindGameCommand(programWriter, reader, gameReadHandler)
            )
    {
    }
}