using GameLister.Services.CommandHandlers.Templates;

namespace GameLister.Models.CommandsHolders;

public interface ICommandsHolder
{
    public ICommandHandler[] Commands { get; }
}
