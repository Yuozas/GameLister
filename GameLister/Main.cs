using GameLister.Models.GameListHolders;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters;

LifeHandler lifeHandler = new();
ConsoleWriter writer = new();
ConsoleReader reader = new();
var manualLister = new ManualGameListerHolder(lifeHandler, writer, reader);
var steamLister = new SteamGameListerHolder(lifeHandler, writer, reader);

ProgramHandler programHandler = new(lifeHandler, writer, reader, manualLister, steamLister);

programHandler.Run();
