using GameLister.Models.Commands;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandHandlers.Templates;

public abstract class CommandHandler : ICommandHandler
{
    protected IProgramWriter Writer;
    protected virtual string BadResponse { get; } = string.Empty;
    protected CommandHandler(IProgramWriter writer)
    {
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
