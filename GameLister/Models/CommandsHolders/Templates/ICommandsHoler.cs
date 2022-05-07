using GameLister.Services.CommandHandlers.Templates;

namespace GameLister.Models.CommandsHolders;

public interface ICommandsHolder
{
    public List<ICommandHandler> Commands { get; }
}
