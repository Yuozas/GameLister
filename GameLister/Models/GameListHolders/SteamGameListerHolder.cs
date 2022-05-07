using GameLister.Models.CommandsHolders;
using GameLister.Models.GameListHolders.Templates;
using GameLister.Services.CommandHandlers.SaveSecrectsCommands;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;
using SteamAPIClient.Helpers.Api;
using SteamAPIClient.Helpers.Api.Templates;
using SteamAPIClient.Services.Api;
using SteamAPIClient.Services.Steam;
using SteamAPIClient.Services.Steam.Templates;

namespace GameLister.Models.GameListHolders;

public class SteamGameListerHolder : GameListerHolder
{
    private readonly IGameListHandler _gameListHandler;
    private readonly IReadProperties _readProperties;
    private readonly IWriteProperties _writeProperties;
    private readonly IProgramWriter _programWriter;
    private readonly IProgramReader _programReader;
    private ICommandsHolder? _emptyFileCommandsHolder;
    private ICommandsHolder? _defaultCommandsHolder;

    public override ICommandsHolder CommandsHolder 
    { 
        get
        {
            if(_defaultCommandsHolder is null)
            {
                BuildCommandsHolder();
                if (_defaultCommandsHolder is null)
                {
                    return _emptyFileCommandsHolder ?? throw new Exception("Empty file commands holder not set.");
                }
            }
            return _defaultCommandsHolder;
        }
    }

    public override IFileCreateHandler FileCreateHandler => _gameListHandler;
    public override string Name => "steam game lister";

    public SteamGameListerHolder(LifeHandler lifeHandler, IProgramWriter programWriter, IProgramReader programReader)
        : base(lifeHandler, programWriter, programReader)
    {
        _gameListHandler = new SteamFileGameListHandler();
        PropertiesReadService propertiesReadService = new();
        _readProperties = propertiesReadService;
        _writeProperties = propertiesReadService;
        _programWriter = programWriter;
        _programReader = programReader;
    }

    private void BuildCommandsHolder()
    {
        var properties = _readProperties.GetProperties();
        if (properties.Key == string.Empty)
        {
            _emptyFileCommandsHolder = new CommandsHolder(new SaveSteamApiPropertiesCommand(_programWriter, _programReader, _writeProperties));
            return;
        }

        ISteamUrlBuilder steamUrlBuilder = new UrlBuilder(properties);
        IPlayerService playerService = new PlayerService(steamUrlBuilder);
        _defaultCommandsHolder = new SteamCommandsHolder(LifeHandler, _programWriter, _programReader,
            _gameListHandler, _gameListHandler, _gameListHandler, playerService);
    }
}