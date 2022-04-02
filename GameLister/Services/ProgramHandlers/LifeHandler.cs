namespace GameLister.Services.ProgramHandlers;

public class LifeHandler
{
    public bool Run { get; private set; } = true;

    public void Exit(string? reason = null)
    {
        if (reason is not null)
            Console.WriteLine(reason);
        Run = false;
    }
}
