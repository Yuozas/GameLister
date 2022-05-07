using GameLister.Data;
using GameLister.Models.CommandsHolders;
using GameLister.Models.GameListHolders.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Models.GameListHolders;

public class ManualGameListerHolder : GameListerHolder
{
    private readonly IGameListHandler _gameListHandler;
    public override ICommandsHolder CommandsHolder { get; }
    public override IFileCreateHandler FileCreateHandler => _gameListHandler;
    public override string Name => "manual game lister";

    public ManualGameListerHolder(LifeHandler lifeHandler, IProgramWriter programWriter, IProgramReader programReader) 
        : base(lifeHandler, programWriter, programReader)
    {
        _gameListHandler = new JsonFileGameListHandler();
        CommandsHolder = new FullGameListCommandsHolder(lifeHandler, programWriter, programReader, 
            _gameListHandler, _gameListHandler, _gameListHandler);
    }
}