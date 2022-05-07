using GameLister.Services.CommandHandlers.FetchGamesCommands;
using GameLister.Services.CommandHandlers.FindGameCommands;
using GameLister.Services.CommandsHandlers.DeleteAccountCommands;
using GameLister.Services.CommandsHandlers.ExitProgramCommands;
using GameLister.Services.CommandsHandlers.ListGamesCommands;
using GameLister.Services.CommandsHandlers.SaveAccountCommands;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;
using SteamAPIClient.Services.Steam.Templates;

namespace GameLister.Models.CommandsHolders;

public class SteamCommandsHolder : CommandsHolder
{
    public SteamCommandsHolder(LifeHandler lifeHandler, IProgramWriter programWriter, IProgramReader reader,
        IAccountWriteHandler accountHandler, IGameReadHandler gameReadHandler, IGameWriteHandler gameWriteHandler,
        IPlayerService playerService) :
        base(
            new ExitProgramCommand(lifeHandler, programWriter),
            new ListGamesCommand(programWriter, gameReadHandler),
            new SaveSteamAccountCommand(programWriter, reader, accountHandler),
            new DeleteAccountCommand(programWriter, reader, accountHandler),
            new FindGameCommand(programWriter, reader, gameReadHandler),
            new SteamFetchGamesCommand(programWriter, gameReadHandler, gameWriteHandler, playerService)
            )
    {
    }
}