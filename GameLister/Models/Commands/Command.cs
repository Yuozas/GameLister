namespace GameLister.Models.Commands;

public readonly struct Command : IEquatable<Command>
{
    public readonly string Name { get; init; }
    public readonly string Description { get; init; }

    public bool Equals(Command other)
    {
        return other.Name == Name;
    }

    public static explicit operator Command(string command) => new() { Name = command };

    public override bool Equals(object? obj)
    {
        return obj is Command command && Equals(command);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public static bool operator ==(Command left, Command right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Command left, Command right)
    {
        return !(left == right);
    }
}
