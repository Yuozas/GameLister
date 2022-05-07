using GameLister.Models.CommandsHolders;
using GameLister.Models.GameListHolders;
using GameLister.Models.GameListHolders.Templates;
using GameLister.Services.CommandHandlers.CommandsHandlerCommands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers;
using GameLister.Services.CommandsHandlers.Templates;
using GameLister.Services.ProgramWriters;

namespace GameLister.Services.ProgramHandlers
{
    public class ProgramHandler
    {
        private readonly LifeHandler _lifeHandler;
        private readonly ConsoleWriter _writer;
        private readonly ConsoleReader _reader;
        private readonly GameListerHolder[] _gameListerHolders;
        private readonly GameListHolderHolder _gameListHolderHolder;
        private readonly CommandHandler _startCommandHandler;
        private readonly CommandHandler _switchCommandHandler;
        public ProgramHandler(LifeHandler lifeHandler, ConsoleWriter writer, ConsoleReader reader, 
            params GameListerHolder[] gameListerHolders)
        {
            _lifeHandler = lifeHandler;
            _writer = writer;
            _reader = reader;
            _gameListerHolders = gameListerHolders;
            _gameListHolderHolder = new();
            _startCommandHandler = new CommandsHandlerSwitchCommandsCommand(_writer, _reader, _gameListerHolders, _gameListHolderHolder, "start command lister");
            _switchCommandHandler = new CommandsHandlerSwitchCommandsCommand(_writer, _reader, _gameListerHolders, _gameListHolderHolder);
            foreach (var gameListerHolder in gameListerHolders)
                gameListerHolder.CommandsHolder.Commands.Insert(0, _switchCommandHandler);
        }

        public void Run()
        {
            while (_lifeHandler.Run)
                GetCommandsHandler().ReadCommand();
        }

        private ICommandsHandler GetCommandsHandler()
        {
            if (_gameListHolderHolder.GameListerHolder is null)
                return new GameListCommandsHandler(_writer, new CommandsHolder(_startCommandHandler));
            return new GameListCommandsHandler(_writer, _gameListHolderHolder.GameListerHolder.CommandsHolder,
                _gameListHolderHolder.GameListerHolder.FileCreateHandler);
        }
    }
}
