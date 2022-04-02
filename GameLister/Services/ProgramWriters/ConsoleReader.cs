using GameLister.Services.ProgramWriters.Templates;

namespace GameLister.Services.ProgramWriters
{
    public class ConsoleReader : IProgramReader
    {
        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public string ReadLineDeclineEmpty(IProgramWriter writer)
        {
            while(true)
            {
                var line = ReadLine();
                if(line is null)
                    writer.WriteLine("No empty lines allowed");
                else
                    return line;
            }
        }
    }
}
