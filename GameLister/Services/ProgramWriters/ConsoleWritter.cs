using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.ProgramWriters;

public class ConsoleWriter : IProgramWriter
{
    public void Clear()
    {
        Console.Clear();
    }

    public void Write(string? text)
    {
        Console.Write(text);
    }

    public void WriteLine(string? text)
    {
        Console.WriteLine(text);
    }
}
