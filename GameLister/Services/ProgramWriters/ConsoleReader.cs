using GameLister.Services.ProgramWriters.Templates;
using System.Text;

namespace GameLister.Services.ProgramWriters;

public class ConsoleReader : IProgramReader
{
    public ConsoleReader(Encoding encoding)
    {
        Console.OutputEncoding = encoding;
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public string ReadLineDeclineEmpty(IProgramWriter writer)
    {
        while (true)
        {
            var line = ReadLine();
            if (line is null)
                writer.WriteLine("No empty lines allowed.");
            else
                return line;
        }
    }

    public string ReadLineDeclineIncorrect(IProgramWriter writer, params string[] options)
    {
        while (true)
        {
            var line = ReadLine();
            if (line is null)
                writer.WriteLine("No empty lines allowed.");
            else if (!options.Contains(line))
                writer.WriteLine("No such option.");
            else
                return line;
        }
    }

    public long ReadNumberLineDeclineEmpty(IProgramWriter writer)
    {
        while (true)
        {
            var line = ReadLine();
            if (line is null)
                writer.WriteLine("No empty lines allowed.");
            else if (!long.TryParse(line, out var number))
                writer.WriteLine("Only numbers allowed.");
            else
                return number;
        }
    }
}
