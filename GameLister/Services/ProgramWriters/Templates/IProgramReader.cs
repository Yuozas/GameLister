namespace GameLister.Services.ProgramWriters.Templates;

public interface IProgramReader
{
    public string? ReadLine();
    public string ReadLineDeclineEmpty(IProgramWriter writer);
    string ReadLineDeclineIncorrect(IProgramWriter writer, params string[] options);
    public long ReadNumberLineDeclineEmpty(IProgramWriter writer);
}
