namespace GameLister.Services.ProgramWriters.Templates;

public interface IProgramReader
{
    public string? ReadLine();
    public string ReadLineDeclineEmpty(IProgramWriter writer);
}
