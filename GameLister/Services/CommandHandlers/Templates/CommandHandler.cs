using GameLister.Models.Commands;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandHandlers.Templates;

public abstract class CommandHandler : ICommandHandler
{
    protected readonly LifeHandler LifeHandler;
    protected IProgramWriter Writer;
    protected virtual string BadResponse { get; } = string.Empty;
    protected CommandHandler(LifeHandler lifeHandler, IProgramWriter writer)
    {
        LifeHandler = lifeHandler;
        Writer = writer;
    }

    public abstract Command Command { get; }

    public abstract void Run();

    public void RespondBad()
    {
        RespondBad(BadResponse);
    }

    public void RespondBad(string response)
    {
        Writer.Clear();
        Writer.WriteLine(response);
    }
}
