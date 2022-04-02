using GameLister.Models.Games;

namespace GameLister.Models.Accounts;

public readonly struct GamesOwner
{
    public readonly Account Account { get; init; }
    public readonly Game[] Games { get; init; }
}
