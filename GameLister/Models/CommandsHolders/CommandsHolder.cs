using GameLister.Services.CommandHandlers.Templates;

namespace GameLister.Models.CommandsHolders;

public class CommandsHolder : ICommandsHolder
{
    public List<ICommandHandler> Commands { get; }

    public CommandsHolder(params ICommandHandler[] commands)
    {
        Commands = commands.ToList();
    }
}