namespace GameLister.Services.ProgramWriters.Templates;

public interface IProgramWriter
{
    public void Write(string? text);
    public void WriteLine(string? text);
    public void Clear();

    public virtual void Write()
    {
        Write(null);
    }

    public virtual void WriteLine()
    {
        WriteLine(null);
    }
}
