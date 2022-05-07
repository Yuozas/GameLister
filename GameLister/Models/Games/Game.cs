namespace GameLister.Models.Games;

public readonly struct Game
{
    public readonly string Name { get; init; }
    public static explicit operator Game(string name) => new() { Name = name};
}