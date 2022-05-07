using GameLister.Models.GameListHolders;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters;
using System.Text;

LifeHandler lifeHandler = new();
ConsoleWriter writer = new(Encoding.Unicode);
ConsoleReader reader = new(Encoding.Unicode);
var manualLister = new ManualGameListerHolder(lifeHandler, writer, reader);
var steamLister = new SteamGameListerHolder(lifeHandler, writer, reader);

ProgramHandler programHandler = new(lifeHandler, writer, reader, manualLister, steamLister);

programHandler.Run();
