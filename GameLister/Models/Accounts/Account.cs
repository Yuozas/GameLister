namespace GameLister.Models.Accounts;

public readonly struct Account
{
    public readonly string Name { get; init; }

    public static explicit operator Account(string name) => new() { Name = name };
}
