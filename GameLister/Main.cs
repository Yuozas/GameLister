using GameLister.Data;
using GameLister.Services.CommandsHandlers;
using GameLister.Services.CommandsHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters;

LifeHandler lifeHandler = new();
ConsoleWriter writer = new();
ConsoleReader reader = new();
JsonFileGameListHandler fileHandler = new();
GameListCommandsHolderData holder = new(lifeHandler, writer, reader, fileHandler);
ICommandsHandler commandsHandler = new GameListCommandsHandler(writer, holder, fileHandler);

while (lifeHandler.Run)
    commandsHandler.ReadCommand();
