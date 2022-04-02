using GameLister.Models.Commands;

namespace GameLister.Services.CommandHandlers.Templates;

public interface ICommandHandler
{
    public Command Command { get; }
    public void Run();
}
