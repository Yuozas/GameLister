using GameLister.Services.ProgramWriters.Templates;
using System.Text;

namespace GameLister.Services.ProgramWriters;

public class ConsoleWriter : IProgramWriter
{
    public ConsoleWriter(Encoding encoding)
    {
        Console.InputEncoding = encoding;
    }

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
