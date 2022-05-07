using GameLister.Models.Commands;
using GameLister.Models.CommandsHolders;
using GameLister.Services.CommandsHandlers.Templates;
using GameLister.Services.GameListHandlers;
using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.CommandsHandlers;

public class GameListCommandsHandler : ICommandsHandler
{
    private const string HELP_COMMAND_1 = "help";
    private const string HELP_COMMAND_2 = "h";
    private const string HELP_DESCRIPTION = "List all commands.";
    private readonly Dictionary<Command, Action> _commands;
    private readonly IProgramWriter _writer;

    public GameListCommandsHandler(IProgramWriter writer, ICommandsHolder commandsHolder, 
        IFileCreateHandler? fileCreateHandler = null)
    {
        _writer = writer;

        if(fileCreateHandler is not null)
            fileCreateHandler.CreateFileIfDoesntExist();

        _commands = new()
        {
            [new() { Name = HELP_COMMAND_1, Description = HELP_DESCRIPTION }] = Help,
            [new() { Name = HELP_COMMAND_2, Description = HELP_DESCRIPTION }] = Help,
        };

        var commandHandlers = commandsHolder.Commands;
        foreach (var commandHandler in commandHandlers)
            _commands.Add(commandHandler.Command, commandHandler.Run);

    }

    public void ReadCommand()
    {
        _writer.WriteLine();
        _writer.WriteLine();
        _writer.WriteLine("Please enter your command.");
        var command = Console.ReadLine();
        Run(command);
    }

    private void Run(string? command)
    {
        if (command is null)
        {
            Ignore(command);
        }
        else if (_commands.TryGetValue((Command)command, out var action))
        {
            _writer.Clear();
            action();
        }
        else
        {
            Ignore(command);
        }
            
    }

    private void Help()
    {
        foreach (var command in _commands.Keys)
            _writer.WriteLine($"{command.Name} - {command.Description}");
    }

    private void Ignore(string? command)
    {
        _writer.Clear();
        _writer.WriteLine($"Such '{command}' command doesn't exist.");
        _writer.WriteLine("Please ensure you enter a valid command.");
        _writer.WriteLine();
        Help();
    }
}
