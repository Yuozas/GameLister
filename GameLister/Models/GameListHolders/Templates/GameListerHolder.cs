using GameLister.Models.CommandsHolders;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Models.GameListHolders.Templates
{
    public abstract class GameListerHolder
    {
        protected LifeHandler LifeHandler { get; }
        protected IProgramWriter ProgramWriter { get; }
        protected IProgramReader ProgramReader { get; }
        public abstract string Name { get; }
        public abstract ICommandsHolder CommandsHolder { get; }
        public abstract IFileCreateHandler FileCreateHandler { get; }

        public GameListerHolder(LifeHandler lifeHandler, IProgramWriter programWriter, IProgramReader programReader)
        {
            LifeHandler = lifeHandler;
            ProgramWriter = programWriter;
            ProgramReader = programReader;
        }
    }
}
