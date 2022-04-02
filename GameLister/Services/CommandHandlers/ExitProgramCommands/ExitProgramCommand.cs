using GameLister.Models.Commands;
using GameLister.Services.CommandHandlers.Templates;
using GameLister.Services.CommandsHandlers.ExitProgramCommands.Templates;
using GameLister.Services.ProgramHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers.ExitProgramCommands;

public class ExitProgramCommand : CommandHandler, IExitProgramCommand
{
    public ExitProgramCommand(LifeHandler lifeHandler, IProgramWriter writer) : base(lifeHandler, writer)
    {
    }

    public override Command Command { get; } = new() { Name = "exit", Description = "Stop the program." };

    public override void Run()
    {
        Writer.WriteLine("Program terminated succesfully.");
        LifeHandler.Exit();
    }
}
